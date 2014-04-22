using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Web.UI.Navigation
{
    /// <summary>
    /// This exception is thrown when section node with given ID cannot be found in the default or provided layout.
    /// </summary>
    class NavigationSectionNotFoundException : System.Exception
    {
        public NavigationSectionNotFoundException(string sectionID,string layoutName) : base(GetCustomMessage(sectionID,layoutName)) { }
        
        private static string GetCustomMessage(string sectionID,string layoutName){
        
            return  string.Format("Section node with ID attribute '{0}' could not be found in layout '{1}'",sectionID,layoutName);
        }
    }
}
