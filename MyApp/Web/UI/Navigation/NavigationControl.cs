using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
 
namespace MyApp.Web.UI.Navigation
{
    [ToolboxData("<{0}:NavigationControl runat=server></{0}:NavigationControl>")]
    public class NavigationControl : WebControl
    {

        private const string CONTROL_TAG = "div";
        private const string DEFAULT_LAYOUT = "default";
        private TransformOptions options;
        private NavigationLayout layout;

        /// <summary>
        /// XML layout file name without extension that will be processed by XSL to render HTML.If this attribute is not set control will look for a layout file 'default.xml'.
        /// </summary>
        public string LayoutName { get; set; }

        /// <summary>
        /// HTML tag that should be contain the layout markup.Default is div
        /// </summary>
        public string ContainerTag { get; set; }
        /// <summary>
        /// Persists navigation state on page load
        /// </summary>
        public bool PersistSectionsState { get; set; }

        /// <summary>
        /// If true navigation behaves like a accordion.Only one section remains open at a time.
        /// </summary>
        public bool Accordion { get; set; }

        /// <summary>
        /// Static host from which static content embedded in control must be loaded.
        /// </summary>
        public string StaticHost { get; set; }

        protected override void RenderContents(HtmlTextWriter output)
        {

            output.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            output.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);

            foreach (string key in this.Style.Keys) {

                output.AddStyleAttribute(key, this.Style[key]);                
            }
                                    
            output.RenderBeginTag(this.options.ContainerTag);

            output.Write(layout.TransformedMarkup);

            output.RenderEndTag();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderContents(writer);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string defaultLayoutDirectory = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data", "NavigationLayouts");

            this.layout = new NavigationLayout(defaultLayoutDirectory);
        }

        public Controls.Section GetNavigationSectionByID(string ID) {

            return this.layout.GetSectionByID(ID);
        }
        /// <summary>
        /// This method transforms the XML layout to HTML and renders the HTML of web control.Only Call this method if there are any runtime modifications in layout otherwise
        /// control will automatically render HTML from XML layout.
        /// </summary>
        public void Transform() {

            this.layout.Transform(this.options);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.options = new TransformOptions();

            this.options.Accordion = this.Accordion;
            this.options.ContainerTag = this.ContainerTag ?? CONTROL_TAG;
            this.options.LayoutName = this.LayoutName ?? DEFAULT_LAYOUT;
            this.options.PersistExpandedState = this.PersistSectionsState;
            this.options.StaticHost = this.StaticHost ?? string.Empty;
            
            layout.Transform(options);
        }

    }
}
