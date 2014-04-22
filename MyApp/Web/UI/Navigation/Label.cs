using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyApp.Web.UI.Navigation.Controls
{

    /// <summary>
    /// This class belong to children of Section node of XML layout that responsible for rendering a Label for Section.
    /// </summary>
    public class Label : GenericItem
    {
        public Label(XmlElement element) : base(element) { }
        
    }
}