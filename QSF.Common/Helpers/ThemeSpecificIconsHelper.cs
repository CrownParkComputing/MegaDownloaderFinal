using System;
using Telerik.Windows.Controls.QuickStart.Common.Helpers;

namespace Telerik.Windows.Controls.QuickStart.Helpers
{
    public static class ThemeSpecificIconsHelper
    {
        public static void SubscribeForIconChanges(bool hasTwoIconSets = false)
        {
            if (hasTwoIconSets)
            {
                ApplicationThemeManager.GetInstance().ThemeChanged += ThemeChanged_TwoVariations;
            }
            else
            {
                ApplicationThemeManager.GetInstance().ThemeChanged += ThemeChanged_ThreeVariations;
            }
        }

        public static void UnsubscribeFromIconChanges(bool hasTwoIconSets = false)
        {
            if (hasTwoIconSets)
            {
                ApplicationThemeManager.GetInstance().ThemeChanged -= ThemeChanged_TwoVariations;
            }
            else
            {
                ApplicationThemeManager.GetInstance().ThemeChanged -= ThemeChanged_ThreeVariations;
            }
        }

        private static void ThemeChanged_ThreeVariations(object sender, EventArgs e)
        {
            ApplyThemeSpecificIconsWithThreeVariations();
        }

        private static void ThemeChanged_TwoVariations(object sender, EventArgs e)
        {
            ApplyThemeSpecificIconsWithTwoVariations();
        }

        public static void ApplyThemeSpecificIconsWithThreeVariations()
        {
            string currentTheme = ApplicationThemeManager.GetInstance().CurrentTheme;

            switch (currentTheme)
            {
                case "Expression_Dark":
                case "VisualStudio2013_Dark":
                case "Green_Dark":
                case "Material_Dark":
                case "Fluent_Dark":
                case "Crystal_Dark":
                case "VisualStudio2019_Dark":
                case "Office2019_Dark":
                case "Office2019_HighContrast":
                    IconSources.ChangeIconsSet(IconsSet.Dark);
                    break;
                case "Green_Light":
                case "Office2013":
                case "Office2016":
                case "Office2016Touch":
                case "Office2013_LightGray":
                case "Office2013_DarkGray":
                case "VisualStudio2013":
                case "VisualStudio2013_Blue":
                case "Windows8":
                case "Windows8Touch":
                case "Material":
                case "Fluent_Light":
                case "Crystal_Light":
                case "VisualStudio2019":
                case "Office2019":
                case "Office2019_Gray":
                    IconSources.ChangeIconsSet(IconsSet.Modern);
                    break;

                default:
                    IconSources.ChangeIconsSet(IconsSet.Light);
                    break;
            }
        }

        public static void ApplyThemeSpecificIconsWithTwoVariations()
        {
            string currentTheme = ApplicationThemeManager.GetInstance().CurrentTheme;

            switch (currentTheme)
            {
                case "Expression_Dark":
                case "VisualStudio2013_Dark":
                case "Green_Dark":
                case "Material_Dark":
                case "Fluent_Dark":
                case "Crystal_Dark":
                case "VisualStudio2019_Dark":
                case "Office2019_Dark":
                case "Office2019_HighContrast":
                    IconSources.ChangeIconsSet(IconsSet.Dark);
                    break;
                default:
                    IconSources.ChangeIconsSet(IconsSet.Light);
                    break;
            }
        }
    }
}
