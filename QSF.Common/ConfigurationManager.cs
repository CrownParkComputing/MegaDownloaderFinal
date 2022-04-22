using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;
using System.Linq;
using System.Xml.Linq;

// Assume that since Silverlight is no longer supported, this system class and namespace will never be introduced in the 
// framework and there will not be any eventual conflict
namespace System.Configuration
{
    /// <summary>
    /// Access appSettings from a configuration file
    /// </summary>
    /// <remarks>Your appConfig file must be in the root of your applcation</remarks>
    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            AppSettings = new Dictionary<string, string>();
            ReadSettings();
        }

        public static Dictionary<string, string> AppSettings { get; set; }

        private static void ReadSettings()
        {
            // Get the name of the executing assemby - we are going to be looking in the root folder for
            // a file called app.config
            string assemblyName = Assembly.GetExecutingAssembly().FullName;
            assemblyName = assemblyName.Substring(0, assemblyName.IndexOf(','));
            string url = String.Format("{0};component/app.config", assemblyName);

            StreamResourceInfo configFile = Application.GetResourceStream(new Uri(url, UriKind.Relative));

            if (configFile != null && configFile.Stream != null)
            {
                Stream stream = configFile.Stream;
                XDocument document = XDocument.Load(stream);

                foreach (XElement element in document.Descendants("appSettings").DescendantNodes().OfType<XElement>())
                {
                    AppSettings.Add(element.Attribute("key").Value, element.Attribute("value").Value);
                }
            }
        }
    }
}