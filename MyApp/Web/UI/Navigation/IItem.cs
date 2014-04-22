using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyApp.Web.UI.Navigation.Controls
{
    /// <summary>
    /// Binds the basic contract for navigational items. 
    /// </summary>
    public interface IItem
    {        
        string Title { get; set; }
        string ToolTip { get; set; }
        bool Visible { get; set; }        
        string IconClass { get; set; }
    }
}