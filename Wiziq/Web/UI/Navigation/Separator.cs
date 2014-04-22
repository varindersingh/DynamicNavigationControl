using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyApp.Web.UI.Navigation.Controls
{

    /// <summary>
    /// This is simplest item in a section which renders a  visual separator(horizontal line) in the section.
    /// </summary>
    public class Separator : GenericItem
    {
        public Separator() { }

        public Separator(XmlElement element) : base(element) { }
    }
}