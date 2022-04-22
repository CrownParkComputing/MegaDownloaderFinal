using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Telerik.Windows.Controls.QuickStart.Common.Helpers
{
    public class ApplicationThemeManager
    {
        public const string DefaultThemeName = "Fluent_Light";

        private const string Office2013DefaultThemeName = "Office2013";
        private const string Office2013LightGrayThemeName = "Office2013_LightGray";
        private const string Office2013DarkGrayThemeName = "Office2013_DarkGray";

        private const string VisualStudio2013DefaultThemeName = "VisualStudio2013";
        private const string VisualStudio2013DarkThemeName = "VisualStudio2013_Dark";
        private const string VisualStudio2013BlueThemeName = "VisualStudio2013_Blue";

        private const string GreenDefaultThemeName = "Green";
        private const string GreenLightThemeName = "Green_Light";
        private const string GreenDarkThemeName = "Green_Dark";

        private const string MaterialDefaultThemeName = "Material";
        private const string MaterialDarkThemeName = "Material_Dark";

        private const string FluentDefaultThemeName = "Fluent";
        private const string FluentDarkThemeName = "Fluent_Dark";
        private const string FluentLightThemeName = "Fluent_Light";

        private const string CrystalDefaultThemeName = "Crystal";
        private const string CrystalDarkThemeName = "Crystal_Dark";
        private const string CrystalLightThemeName = "Crystal_Light";

        private const string VisualStudio2019DefaultThemeName = "VisualStudio2019";
        private const string VisualStudio2019DarkThemeName = "VisualStudio2019_Dark";
        private const string VisualStudio2019BlueThemeName = "VisualStudio2019_Blue";

        private const string Office2019DefaultThemeName = "Office2019";
        private const string Office2019GrayThemeName = "Office2019_Gray";
        private const string Office2019DarkThemeName = "Office2019_Dark";
        private const string Office2019LightThemeName = "Office2019_Light";
        private const string Office2019HighContrastThemeName = "Office2019_HighContrast";

        public event EventHandler ThemeChanged;

        private static readonly ApplicationThemeManager instance = new ApplicationThemeManager();

        // contains items like <"/Telerik.Windows.Themes.Windows8;component/telerik.windows.controls.xaml", ResourceDictionary>
        private static readonly Dictionary<string, ResourceDictionary> cachedResourceDictionaries = new Dictionary<string, ResourceDictionary>();

        private static readonly string[] defaultReferencesNamesForApplication = new string[]
        {
            "System.Windows",
            "Telerik.Windows.Controls",
            "Telerik.Windows.Controls.Input",
            "Telerik.Windows.Controls.Navigation",
            "Telerik.Windows.Controls.SyntaxEditor"
        };

        private string currentTheme;

        public string CurrentTheme
        {
            get
            {
                return this.currentTheme;
            }
        }

        private ApplicationThemeManager()
        {
        }

        public static ApplicationThemeManager GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Adds ResourceDictionary items to application resources for certain theme using assembly names to form the ResourceDictionary's Source
        /// </summary>
        /// <param name="themeName">The theme for which resources to add</param>
        /// <param name="resourcesPaths">String array of assembly names used to form the Source of the ResourceDictionary items</param>
        public void EnsureResourcesForTheme(string themeName, string[] resourcesPaths = null)
        {
            if (resourcesPaths == null)
            {
                resourcesPaths = new string[] { };
            }

            // always include default resources
            resourcesPaths = defaultReferencesNamesForApplication.Union(resourcesPaths).ToArray();

            Action resetApplicationResources = () =>
            {
                var currentDictionaries = Application.Current.Resources.MergedDictionaries.ToList();
                foreach (var item in currentDictionaries)
                {
                    // Remove only the current control resource dictionaries 
                    if (item.Source != null && item.Source.OriginalString.Contains("Telerik.Windows.Themes."))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(item);
                    }
                }

                this.currentTheme = themeName;
                themeName = this.GetDefaultThemeNameAndSetColorVariation(themeName);

                // add new resources
                foreach (string resourcePath in resourcesPaths)
                {
                    var name = resourcePath.Split(',')[0].ToLower(CultureInfo.InvariantCulture);

                    var xamlFile = string.Format("{0}.xaml", name);
                    var bamlFile = string.Format("{0}.baml", name);

                    var uriStringToAdd = "/Telerik.Windows.Themes." + themeName + ";component/themes/" + xamlFile;

                    var assembly = System.AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName != null &&
                                                                                             a.FullName.Contains(themeName) &&
                                                                                             !a.FullName.Contains("QuickStart.Theme")).FirstOrDefault();

                    if (assembly == null)
                    {
                        assembly = System.Reflection.Assembly.Load(string.Format("Telerik.Windows.Themes.{0}", themeName));
                    }

                    if (assembly != null)
                    {
                        var paths = ResourceToStreamHelper.GetResourcePaths(assembly);
                        if (!paths.Contains(string.Format("themes/{0}", xamlFile)) && !paths.Contains(string.Format("themes/{0}", bamlFile)))
                        {
                            continue;
                        }
                    }

                    // this call may cause "Collection was modified" exception (WPF)
                    AddDictionaryToApplicationResources(uriStringToAdd);
                }
            };

            // retry to reset resources several times (hides the exception with "Collection was modified")
            int retries = 0;

            while (retries < 5)
            {
                try
                {
                    resetApplicationResources();
                    break;
                }
                catch
                {
                    retries++;
#if DEBUG
                    if (retries > 3)
                        throw;
#endif
                }
            }

            this.OnThemeChanged();
        }

        public static void EnsureFrameworkElementResourcesForTheme(FrameworkElement element, string themeName)
        {
            var resourcesPaths = new string[] { };

            // always include default resources
            resourcesPaths = defaultReferencesNamesForApplication.ToArray();

            // temporarily save QSF's resources
            var telerikThemeDictionaries = from keyValuePair in cachedResourceDictionaries
                                           where keyValuePair.Key.Contains("Telerik.Windows.Themes.")
                                           select keyValuePair.Value;
            var qsfOnlyDictionaries = Application.Current.Resources.MergedDictionaries.Except(telerikThemeDictionaries).ToList();

            element.Resources.MergedDictionaries.Clear();

            // add new resources
            foreach (string resourcePath in resourcesPaths)
            {
                var xamlFile = resourcePath.Split(',')[0].ToLower(CultureInfo.InvariantCulture) + ".xaml";
                var uriStringToAdd = "/Telerik.Windows.Themes." + themeName + ";component/themes/" + xamlFile;

                var uri = new Uri(uriStringToAdd, UriKind.RelativeOrAbsolute);
                var resourceDictionary = new ResourceDictionary() { Source = uri };
                element.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }

        private static void AddDictionaryToApplicationResources(string uriStringToAdd)
        {
            ResourceDictionary resourceDictionary = null;

            // load ResourceDictionary from cache, if exists there, otherwise try to create it and add it to cache
            if (cachedResourceDictionaries.ContainsKey(uriStringToAdd))
            {
                resourceDictionary = cachedResourceDictionaries[uriStringToAdd];
            }
            else
            {
                try
                {
                    var uri = new Uri(uriStringToAdd, UriKind.RelativeOrAbsolute);

                    resourceDictionary = new ResourceDictionary() { Source = uri };
                    cachedResourceDictionaries.Add(uriStringToAdd, resourceDictionary);
                }
                catch
                {
                    resourceDictionary = null;
                    cachedResourceDictionaries.Add(uriStringToAdd, resourceDictionary);
#if DEBUG
                    throw;
#endif
                }
            }

            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }

        private string GetDefaultThemeNameAndSetColorVariation(string themeName)
        {
            // Office2013 Theme
            if (themeName.Contains(ApplicationThemeManager.Office2013DefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.Office2013LightGrayThemeName):
                        {
                            Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.LightGray);
                        }
                        break;
                    case (ApplicationThemeManager.Office2013DarkGrayThemeName):
                        {
                            Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.DarkGray);
                        }
                        break;
                    default:
                        {
                            Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.White);
                        }
                        break;
                }

                return ApplicationThemeManager.Office2013DefaultThemeName;
            }

            // VisualStudio2013 Theme
            if (themeName.Contains(ApplicationThemeManager.VisualStudio2013DefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.VisualStudio2013BlueThemeName):
                        {
                            VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Blue);
                        }
                        break;
                    case (ApplicationThemeManager.VisualStudio2013DarkThemeName):
                        {
                            VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Dark);
                        }
                        break;
                    default:
                        {
                            VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Light);
                        }
                        break;
                }

                return ApplicationThemeManager.VisualStudio2013DefaultThemeName;
            }

            // Green Theme
            if (themeName.Contains(ApplicationThemeManager.GreenDefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.GreenLightThemeName):
                        {
                            GreenPalette.LoadPreset(GreenPalette.ColorVariation.Light);
                        }
                        break;
                    case (ApplicationThemeManager.GreenDarkThemeName):
                        {
                            GreenPalette.LoadPreset(GreenPalette.ColorVariation.Dark);
                        }
                        break;
                    default:
                        {
                            GreenPalette.LoadPreset(GreenPalette.ColorVariation.Dark);
                        }
                        break;
                }

                return ApplicationThemeManager.GreenDefaultThemeName;
            }

            // Material Theme
            if (themeName.Contains(ApplicationThemeManager.MaterialDefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.MaterialDarkThemeName):
                        {
                            MaterialPalette.LoadPreset(MaterialPalette.ColorVariation.Dark);
                        }
                        break;
                    default:
                        {
                            MaterialPalette.LoadPreset(MaterialPalette.ColorVariation.Light);
                        }
                        break;
                }

                return ApplicationThemeManager.MaterialDefaultThemeName;
            }


            // Fluent Theme
            if (themeName.Contains(ApplicationThemeManager.FluentDefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.FluentDarkThemeName):
                        {
                            FluentPalette.LoadPreset(FluentPalette.ColorVariation.Dark);
                        }
                        break;
                    default:
                        {
                            FluentPalette.LoadPreset(FluentPalette.ColorVariation.Light);
                        }
                        break;
                }

                return ApplicationThemeManager.FluentDefaultThemeName;
            }

            // Crystal Theme
            if (themeName.Contains(ApplicationThemeManager.CrystalDefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.CrystalDarkThemeName):
                        {
                            CrystalPalette.LoadPreset(CrystalPalette.ColorVariation.Dark);
                        }
                        break;
                    default:
                        {
                            CrystalPalette.LoadPreset(CrystalPalette.ColorVariation.Light);
                        }
                        break;
                }

                return ApplicationThemeManager.CrystalDefaultThemeName;
            }

            // VisualStudio2019 Theme
            if (themeName.Contains(ApplicationThemeManager.VisualStudio2019DefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.VisualStudio2019DarkThemeName):
                        {
                            VisualStudio2019Palette.LoadPreset(VisualStudio2019Palette.ColorVariation.Dark);
                        }
                        break;
                    default:
                        {
                            VisualStudio2019Palette.LoadPreset(VisualStudio2019Palette.ColorVariation.Blue);
                        }
                        break;
                }

                return ApplicationThemeManager.VisualStudio2019DefaultThemeName;
            }

            // Office2019 Theme
            if (themeName.Contains(ApplicationThemeManager.Office2019DefaultThemeName))
            {
                switch (themeName)
                {
                    case (ApplicationThemeManager.Office2019GrayThemeName):
                        {
                            Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.Gray);
                        }
                        break;
                    case (ApplicationThemeManager.Office2019DarkThemeName):
                        {
                            Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.Dark);
                        }
                        break;
                    case (ApplicationThemeManager.Office2019HighContrastThemeName):
                        {
                            Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.HighContrast);
                        }
                        break;
                    default:
                        {
                            Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.Light);
                        }
                        break;
                }
                return ApplicationThemeManager.Office2019DefaultThemeName;
            }

            return themeName;
        }

        private void OnThemeChanged()
        {
            if (ThemeChanged != null)
            {
                this.ThemeChanged(this, EventArgs.Empty);
            }
        }
    }
}