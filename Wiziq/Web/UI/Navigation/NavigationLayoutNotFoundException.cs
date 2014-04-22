using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyApp.Web.UI.Navigation
{

    /// <summary>
    /// This exception is thrown when either no layout is provided to control and default layout is not present or a layout is provided but no coressponding layout file could be found.
    /// </summary>
    public class NavigationLayoutXMLNotFoundException : System.Exception
    {
        public NavigationLayoutXMLNotFoundException(string layoutOrDirectoryName, string layoutOrDirectoryPath, Exception originalException)
            : base(CustomMessage(layoutOrDirectoryName, layoutOrDirectoryPath, originalException))
        {

        }

        private static string CustomMessage(string layoutOrDirectoryName, string layoutOrDirectoryPath, Exception originalException)
        {

            string message = string.Empty;
            
            if (originalException is System.IO.DirectoryNotFoundException) 
                message = string.Format("Navigation layout directory '{0}' not found.\nOriginal error: {1}", layoutOrDirectoryName, originalException.Message);

            if (originalException is System.IO.FileNotFoundException)
            {
                if (string.IsNullOrEmpty(layoutOrDirectoryName))

                    message = string.Format("No XML layout was provided and 'default' layout could not be found at below location{0}", Environment.NewLine + layoutOrDirectoryPath);
                else
                    message = string.Format("XML layout file was not found at given path {0}", Environment.NewLine + layoutOrDirectoryPath);
            }

          
             // message += "\n Root Cause: \n " + originalException.Message;

              return message;
        }

    }
}