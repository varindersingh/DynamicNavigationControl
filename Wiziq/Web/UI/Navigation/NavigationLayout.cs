using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Data;
using System.Configuration;
using System.Xml.Xsl;
using System.Xml.XPath;
using MyApp.Web.UI;
using MyApp.Web.UI.Navigation;
using MyApp.Web.UI.Navigation.Controls;

namespace MyApp.Web.UI.Navigation
{

    /// <summary>
    /// Summary description for Navigation
    /// </summary>
    public class NavigationLayout
    {
        private string _layoutName = null;
        private XmlDocument _docLayout;
        private string _transformedmarkup = string.Empty;
        private string _layoutDirectory = null;

        private const string DEFAULT_LAYOUT = "default";

        private List<Section> _sections;

        public NavigationLayout(string layoutDirectory) : this(null, layoutDirectory) { }


        public NavigationLayout(string layout, string layoutDirectory)
        {
            this._layoutName = layout;
            this._layoutDirectory = layoutDirectory;

            _sections = new List<Section>();

            this._docLayout = new XmlDocument();
            this.Init();
        }

        private void Init()
        {

            string layoutPath = null;

            try
            {
                layoutPath = Path.Combine(_layoutDirectory, string.Format("{0}.xml", _layoutName ?? DEFAULT_LAYOUT));
                _docLayout.Load(layoutPath);
            }
            catch (System.Exception ex)
            {

                if (ex is DirectoryNotFoundException)
                    throw new NavigationLayoutXMLNotFoundException(_layoutDirectory, layoutPath, ex);

                if (ex is FileNotFoundException)
                    throw new NavigationLayoutXMLNotFoundException(_layoutName, layoutPath, ex);

                throw ex;
            }
        }

        private string GetRenderedLayoutCacheKey(string layout)
        {

            return (layout ?? DEFAULT_LAYOUT) + "RenderedNavigationLayoutCache";
        }

        public Section GetSectionByID(string ID)
        {
            Section cachedSec = this._sections.Find(x => x.ID.CompareTo(ID) == 0);

            if (cachedSec != null)
                return cachedSec;

            XmlElement sectionNode = _docLayout.GetElementById(ID);

            if (sectionNode == null)
                throw new NavigationSectionNotFoundException(ID, _layoutName ?? DEFAULT_LAYOUT);

            cachedSec = new Section(sectionNode);

            this._sections.Add(cachedSec);

            return cachedSec;
        }

        public string TransformedMarkup
        {
            get
            {
                object cachedTransformation = System.Web.HttpContext.Current.Cache[GetRenderedLayoutCacheKey(_layoutName)];
                if (cachedTransformation != null)
                    return (string)cachedTransformation;

                return _transformedmarkup;
            }
        }


        private XmlElement BuildXmlElement(XmlDocument document, IItem item)
        {

            XmlElement node = document.CreateElement(item.GetType().Name);

            node.SetAttribute("title", item.Title);
            //node.SetAttribute("count", item.Count.ToString());            
            node.SetAttribute("visible", item.Visible.ToString().ToLower());
            node.SetAttribute("icon-class", item.IconClass);

            return node;
        }


        public void AddItems(string sectionID, List<IItem> items)
        {
            XmlElement dynNav = _docLayout.GetElementById(sectionID);
            if (dynNav != null)
            {
                foreach (IItem item in items)
                {
                    dynNav.AppendChild(BuildXmlElement(_docLayout, item));
                }
            }
        }

        public void Transform(TransformOptions options)
        {

            XslCompiledTransform transform = new XslCompiledTransform();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            Stream ins = null;

            IEnumerable<string> filter = assembly.GetManifestResourceNames().Where(x => x.EndsWith("transform.xsl"));

            if (filter.Any()) {

                ins = assembly.GetManifestResourceStream((string)filter.First());
            }

          
            if (ins == null)
                throw new Exception("transform.xsl stream not found");

            using (XmlReader styleReader = XmlReader.Create(ins))
            {
                transform.Load(styleReader);
            }


            //_docLayout.Save(_layoutDirectory + "\\output.xml");

            XsltArgumentList args = new XsltArgumentList();

            args.AddParam("persistState", string.Empty, options.PersistExpandedState);
            args.AddParam("accordion", string.Empty, options.Accordion);
            args.AddParam("staticHost", string.Empty, options.StaticHost);

#if DEBUG
            args.AddParam("isDevMode", string.Empty, true);
#endif

            MemoryStream ms = new MemoryStream();
            transform.Transform(_docLayout, args, ms);
            ms.Position = 0;

            using (StreamReader rd = new StreamReader(ms))
            {
                _transformedmarkup = rd.ReadToEnd();
            }

            System.Web.HttpContext.Current.Cache.Insert(GetRenderedLayoutCacheKey(_layoutName), _transformedmarkup);
        }
    }


}