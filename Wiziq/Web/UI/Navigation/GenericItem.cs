using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyApp.Web.UI.Navigation.Controls
{

    /// <summary>
    /// Belongs to the child elements of Section node of XML layout.If type of children of Section is not known this type must be used.
    /// For accessing specific properties of individual children elements of Section this type must be casted to appropriate type.
    /// </summary>
    public abstract class GenericItem : IItem
    {              
        protected XmlElement _element;

        public GenericItem() { }

        public GenericItem(XmlElement element)
        {

            if (element.Name != this.GetType().Name)
                throw new Exception("XML Element node name must match class name");

            this._element = element;
            this.Init();
        }


        private void Init()
        {

            this.Title = this._element.GetAttribute("title");
            this.IconClass = this._element.GetAttribute("icon-class");
            bool currentVisisbleByDefault = true;

            if (bool.TryParse(this._element.GetAttribute("visible"), out currentVisisbleByDefault))
                this.Visible = currentVisisbleByDefault;

            this.ToolTip = this._element.GetAttribute("tooltip");
        }

        public GenericItem(string title)
            : this()
        {
            this.Title = title;
        }

        public GenericItem(string title, string iconClass)
        {
            this.Title = title;
            this.IconClass = iconClass;
        }

        /// <summary>
        /// Title of the element that will be actually displayed in browser.
        /// </summary>
        public string Title
        {
            get { return this._element.GetAttribute("title"); }
            set { this._element.SetAttribute("title", value); }
        }

        /// <summary>
        /// CSS class of the item
        /// </summary>
        public string IconClass
        {
            get { return this._element.GetAttribute("icon-class"); }
            set
            {
                if (this._element != null)
                    this._element.SetAttribute("icon-class", value);
            }
        }
        /// <summary>
        /// Shows tooltip text on hover.It is basically the title attribute of HTML element.
        /// </summary>
        public string ToolTip
        {
            get { return this._element.GetAttribute("tooltip"); }
            set
            {
                if (this._element != null)
                    this._element.SetAttribute("tooltip", value);
            }
        }

        /// <summary>
        /// Sets or Gets whether current link is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                bool active = true;
                bool.TryParse(this._element.GetAttribute("active"), out active);
                return active;
            }
            set
            {
                if (this._element != null)
                    this._element.SetAttribute("active", value.ToString().ToLower());
            }
        }


        /// <summary>
        /// Whether this item should be rendered in HTML or not.If false no HTML will be rendered for the item.
        /// </summary>
        public bool Visible
        {
            get { 
                bool visible = true;
                bool.TryParse(this._element.GetAttribute("visible"), out visible);
                return visible;
            }
            set
            {
                if (this._element != null)
                    this._element.SetAttribute("visible", value.ToString().ToLower());
            }
        }

    }
}
