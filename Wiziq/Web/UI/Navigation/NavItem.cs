using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyApp.Web.UI.Navigation.Controls
{
    /// <summary>
    /// This class belong to anchor element in navigation section.It renders a clickable link in a section.
    /// </summary>
    public class NavItem : GenericItem
    {

        private string _href = string.Empty;
        private int _count = 0;

        public NavItem(XmlElement element) : base(element) { }

        public NavItem(string title, string iconClass)
            : this(title, iconClass, string.Empty, 0)
        {

        }

        public NavItem(string title, string iconClass, string href)
            : this(title, iconClass, href, 0)
        {

        }

        public NavItem(string title, string iconClass, string href, int count)
        {
            this.Title = title;
            this.IconClass = iconClass;
            this._href = href;
            this._count = count;
        }

        public string Href
        {
            get
            {
                if (this._element != null)
                    this._element.GetAttribute("href");
                return this._href;
            }

            set
            {
                this._href = value;
                if (this._element != null)
                    this._element.SetAttribute("href", value);
            }
        }
        public int Count
        {
            get
            {
                if (this._element != null)
                    this._element.GetAttribute("count"); return this._count;
            }
            set
            {
                if (this._element != null)
                    this._element.SetAttribute("count", Convert.ToString(value));
                
                this._count = value;
            }
        }

    }
}