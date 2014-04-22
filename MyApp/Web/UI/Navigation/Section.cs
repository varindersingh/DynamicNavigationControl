using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MyApp.Web.UI.Navigation.Controls
{

    /// <summary>
    /// This class belongs to the Section node of XML layout whose purpose is to logically group navigation items such as labels, horizontal spaces and links.  
    /// </summary>
    public class Section : GenericItem, IComparable<Section>
    {

        private List<GenericItem> _items;

        private const string EXPANDED_SEC_ID = "expandedSectionID";

        private Section() { }

        public Section(XmlElement _sectionElement)
            : base(_sectionElement)
        {
            bool currentExpandedByDefault = false;
            if (bool.TryParse(this._element.GetAttribute("expanded"), out currentExpandedByDefault))
                ToggleCurrentSectionExpanded(currentExpandedByDefault);

            this._items = new List<GenericItem>();
        }

        public string ID
        {
            get
            {
                return this._element.GetAttribute("ID");
            }
        }

        public new string IconClass
        {
            get { return this._element.GetAttribute("icon-class"); }
            set { this._element.GetAttribute("icon-class", value); }
        }

        /// <summary>
        /// Get or Sets expanded property of current section.If true section will be expanded on render of control.Only one section can be expanded at a time.If this property is set to true for multiple sections
        /// only last set section will be rendered expanded.
        /// </summary>
        public bool Expanded
        {

            get { return Convert.ToBoolean(this._element.GetAttribute("expanded")); }
            set
            {
                ToggleCurrentSectionExpanded(value);
            }
        }
        /// <summary>
        /// Get or Sets the DisplayIndex property for current section.Sections in expandable group will be displayed ascedending order 
        /// i.e. section having lowest index will be displayed on top.If no index property is set a default index 9999 will be assumed.
        /// </summary>
        public ushort DisplayIndex
        {
            get { return Convert.ToUInt16(this._element.GetAttribute("displayIndex")); }
            set { SetCurrentSectionDisplayIndex(value); }
        }

        private void SetCurrentSectionDisplayIndex(int displayIndex)
        {
            this._element.SetAttribute("displayIndex", Convert.ToString(displayIndex));
        }

        private void SetCalloutVisibility(bool visible)
        {

            XmlNode node = this._element.SelectSingleNode(typeof(Callout).Name);
            if (node != null)
            {
                XmlElement calloutElem = (XmlElement)node;
                calloutElem.SetAttribute("visible", Convert.ToString(visible).ToLower());
            }
        }

        private void ToggleCurrentSectionExpanded(bool expand)
        {

            XmlElement root = this._element.OwnerDocument.DocumentElement;

            string lastExpanded = (root.GetAttribute(EXPANDED_SEC_ID) ?? string.Empty);
            string currentSectionID = this._element.GetAttribute("ID");

            if (expand)
                root.SetAttribute(EXPANDED_SEC_ID, currentSectionID);
            else
                root.SetAttribute(EXPANDED_SEC_ID, (lastExpanded == currentSectionID) ? string.Empty : lastExpanded);
        }

        private Callout GetCalloutItem()
        {

            XmlNode node = this._element.SelectSingleNode(typeof(Callout).Name);
            if (node == null)
                return null;

            XmlElement calloutElem = (XmlElement)node;
            Callout callout = (Callout)Activator.CreateInstance(typeof(Callout), calloutElem);

            callout.Title = calloutElem.GetAttribute("title");
            callout.Text = calloutElem.InnerText;
            callout.ButtonText = calloutElem.GetAttribute("buttonText");

            bool isVisible = false;
            callout.Visible = isVisible;

            if (bool.TryParse(calloutElem.GetAttribute("visible").ToLower(), out isVisible))
                callout.Visible = isVisible;

            return callout;
        }


        public bool ShowCallout
        {
            get
            {
                Callout callout = GetCalloutItem();
                return ( callout == null ? false : callout.Visible );
            }
            set
            {
                SetCalloutVisibility(value);
            }
        }


        /// <summary>
        /// Get &lt;NavItemgt; with title attribute matching the passed parameter title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public NavItem GetNavItemByTitle(string title)
        {

            XmlNode node = this._element.SelectSingleNode(string.Format("*[@title='{0}']", title));

            if (node == null)
                return null;

            XmlElement itemElement = (XmlElement)node;

            NavItem item = (NavItem)Activator.CreateInstance(typeof(NavItem), itemElement);

            item.Title = itemElement.GetAttribute("title");
            item.ToolTip = itemElement.GetAttribute("tooltip");
            bool isVisible = true;

            item.Visible = isVisible;

            if (bool.TryParse(itemElement.GetAttribute("visible").ToLower(), out isVisible))
                item.Visible = isVisible;


            item.IconClass = itemElement.GetAttribute("icon-class");


            return item;
        }

        /// <summary>
        /// Get List of lt;NavItem;gt; objects for current section
        /// </summary>
        /// <returns></returns>
        public List<GenericItem> GetItems()
        {
            XmlNodeList sectionElements = this._element.SelectNodes("*");

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            foreach (XmlNode node in sectionElements)
            {
                Type type = assembly.GetTypes()
                    .First(t => t.Name == node.Name);

                XmlElement itemElement = (XmlElement)node;

                GenericItem item = (GenericItem)Activator.CreateInstance(type, itemElement);

                item.Title = itemElement.GetAttribute("title");
                item.ToolTip = itemElement.GetAttribute("tooltip");
                item.Visible = Convert.ToBoolean(itemElement.GetAttribute("visible").ToLower());
                item.IconClass = itemElement.GetAttribute("icon-class");
                //item.co = itemElement.GetAttribute("title");
                //HttpContext.Current.Response.Write(HttpContext.Current.Server.HtmlEncode(node.OuterXml) + "<br />");
                _items.Add(item);
            }

            return _items;
        }

        public int CompareTo(Section other)
        {
            return this.DisplayIndex - other.DisplayIndex;
        }

    }
}
