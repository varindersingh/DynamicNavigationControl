using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Web.UI.Navigation
{
    public class TransformOptions
    {
        public string LayoutName { get; set; }        
        public bool PersistExpandedState { get; set; }
        public bool Accordion { get; set; }
        public string ContainerTag { get; set; }
        public string StaticHost { get; set; }
    }
}
