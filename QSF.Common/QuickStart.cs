using System.Windows;
using System.Windows.Controls;

namespace Telerik.Windows.Controls.QuickStart
{
    /// <summary>
    /// Container for the ConfigurationPanel attached property used in QuickStart application.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public static class QuickStart
    {
        /// <summary>
        /// Gets the value of the ExampleHeader attached property from a given DependencyObject.
        /// </summary>
        /// <param name="obj">The DependencyObject from which to read the property value.</param>
        /// <returns>The value of the ExampleHeader attached property.</returns>
        public static object GetExampleHeader(DependencyObject obj)
        {
            return obj.GetValue(ExampleHeaderProperty);
        }

        /// <summary>
        /// Sets the value of the ExampleHeader attached property to a given DependencyObject.
        /// </summary>
        /// <param name="obj">The DependencyObject on which to set the property value.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetExampleHeader(DependencyObject obj, object value)
        {
            obj.SetValue(ExampleHeaderProperty, value);
        }

        /// <summary>
        /// Identifies the ExampleHeader attached property.
        /// </summary>
        public static readonly DependencyProperty ExampleHeaderProperty =
            DependencyProperty.RegisterAttached("ExampleHeader", typeof(object), typeof(QuickStart), new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of the ConfigurationPanel attached property from a given DependencyObject.
        /// </summary>
        /// <param name="obj">The DependencyObject from which to read the property value.</param>
        /// <returns>The value of the ConfigurationPanel attached property.</returns>
        public static Panel GetConfigurationPanel(DependencyObject obj)
        {
            return (Panel)obj.GetValue(ConfigurationPanelProperty);
        }

        /// <summary>
        /// Sets the value of the ConfigurationPanel attached property to a given DependencyObject.
        /// </summary>
        /// <param name="obj">The DependencyObject on which to set the property value.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetConfigurationPanel(DependencyObject obj, Panel value)
        {
            obj.SetValue(ConfigurationPanelProperty, value);
        }

        /// <summary>
        /// Identifies the ConfigurationPanel attached property.
        /// </summary>
        public static readonly DependencyProperty ConfigurationPanelProperty =
            DependencyProperty.RegisterAttached("ConfigurationPanel", typeof(Panel), typeof(QuickStart), new PropertyMetadata(null, OnConfigurationPanelChanged));

        private static void OnConfigurationPanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
#if WPF
            var configurationPanel = e.NewValue as Panel;
            if (configurationPanel != null)
            {
                // Set NameScope, so the ElementName bindings can work.
                NameScope.SetNameScope(configurationPanel, NameScope.GetNameScope(d));
            }
#endif
        }
    }
}
