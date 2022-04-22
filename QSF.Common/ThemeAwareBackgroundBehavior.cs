using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Telerik.Windows.Controls.QuickStart
{
    /// <summary>
    /// Telerik QuickStart Framework element background changer.
    /// </summary>
    public static class ThemeAwareBackgroundBehavior
    {
        /// <summary>
        /// Identifies the IsEnabled attached property.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ThemeAwareBackgroundBehavior), new PropertyMetadata(OnIsEnabledPropertyChanged));

        /// <summary>
        /// Returns a bool that specifies whether the behavior is enabled or not.
        /// </summary>
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// Writes the TextPath attached property to the specified element.
        /// </summary>
        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            DependencyProperty backgroundProperty = null;

            var control = d as Control;
            var border = d as Border;
            FrameworkElement element = null;
            if (control != null)
            {
                backgroundProperty = Control.BackgroundProperty;
                element = control;
            }
            else if (border != null)
            {
                backgroundProperty = Border.BackgroundProperty;
                element = border;
            }

            if (backgroundProperty != null)
            {
                RoutedEventHandler elementLoaded = null;
                elementLoaded = (object sender, RoutedEventArgs e) =>
                {
                    element.Loaded -= elementLoaded;
                    FrameworkElement parentUserControl = element.ParentOfType<UserControl>();
                    if (parentUserControl != null && parentUserControl.DataContext != null)
                    {
                        var dataContext = parentUserControl.DataContext;
                        var dataContextType = dataContext.GetType();
                        if (dataContextType.GetProperty("CurrentTheme") == null)
                        {
                            parentUserControl = parentUserControl.Parent as FrameworkElement;
                        }

                        element.SetBinding(backgroundProperty, new Binding("DataContext.CurrentTheme")
                        {
                            Source = parentUserControl,
                            Converter = new ThemeBackgroundConverter(),
                            ConverterParameter = (Brush)d.GetValue(backgroundProperty)
                        });
                    }
                };
                element.Loaded += elementLoaded;
            }
        }

        internal class ThemeBackgroundConverter : IValueConverter
        {
            Random random = new Random();

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                var theme = value.ToString();
                if (!string.IsNullOrEmpty(theme))
                {
                    if (theme.ToLower().Contains("transparent"))
                    {
                        return new ImageBrush() { ImageSource = new BitmapImage(GetTransparentThemeImageUri()), Stretch = Stretch.UniformToFill };
                    }
                    else if (theme.ToLower().Contains("expression_dark"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 28, 28, 28) };
                    }
                    else if (theme.ToLower().Contains("visualstudio2013_dark"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 18, 18, 20) };
                    }
                    else if (theme.ToLower().Contains("green_dark"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 19, 19, 19) };
                    }
                    else if (theme.ToLower().Contains("material_dark"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 18, 18, 18) };
                    }
                    else if (theme.ToLower().Contains("fluent_dark") || theme.ToLower().Contains("crystal_dark"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 0, 0, 0) };
                    }
                    else if (theme.ToLower().Contains("visualstudio2019_dark"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 27, 28, 28) };
                    }
                    else if (theme.ToLower().Contains("office2019_gray")) {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 118, 114, 114) };
                    }
                    else if (theme.ToLower().Contains("office2019_dark"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 15, 15, 15) };
                    }
                    else if (theme.ToLower().Contains("office2019_highcontrast"))
                    {
                        return new SolidColorBrush() { Color = Color.FromArgb(255, 0, 0, 0) };
                    }
                    else
                    {
                        return new SolidColorBrush(Colors.White);
                    }
                }

                if (parameter == null)
                {
                    return DependencyProperty.UnsetValue;
                }

                return parameter;
            }

            private Uri GetTransparentThemeImageUri()
            {
                return new Uri(string.Format("pack://application:,,,/Application;component/Resources/Images/Background0{0}.jpg", random.Next(1, 7)), UriKind.RelativeOrAbsolute);
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return value;
            }
        }
    }
}