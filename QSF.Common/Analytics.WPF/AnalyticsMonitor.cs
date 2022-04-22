using GoogleAnalytics;
using System;
using System.Configuration;
using System.Deployment.Application;
using System.Linq;

namespace Telerik.Windows.Controls.QuickStart.Analytics
{
    public class AnalyticsMonitor
    {
        private static readonly AnalyticsMonitor instance = new AnalyticsMonitor();
        private static GoogleAnalyticsMonitor monitor;
        private static bool isTrackingEnabled;

        private const string AnalyticsProductKey = "analyticsProductKey";
        private const string AppName = "WPF QSF";

        public static AnalyticsMonitor Instance
        {
            get
            {
                return instance;
            }
        }

        public static bool IsAnalyticsDisabledInRegistry
        {
            get
            {
                return AnalyticsConfiguration.IsAnalyticsDisabledInRegistry;
            }
        }

        public bool IsTrackingEnabled
        {
            get
            {
                return isTrackingEnabled;
            }
            set
            {
                isTrackingEnabled = value && !IsAnalyticsDisabledInRegistry;
                if (isTrackingEnabled)
                {
                    TryInitializeMonitor();
                }
                else
                {
                    if (monitor != null)
                    {
                        monitor = null;
                    }
                }
            }
        }

        public void TrackException(Exception exception, string contextMessage = null)
        {
            if (!this.IsTrackingEnabled || monitor == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(contextMessage))
            {
                monitor.TrackError(string.Empty, exception);
            }
            else
            {
                monitor.TrackError(contextMessage, exception);
            }
        }

        public void TrackFeature(string featureName)
        {
            if (!this.IsTrackingEnabled || monitor == null)
            {
                return;
            }

            monitor.TrackAtomicFeature(featureName);
        }

        public void TrackFeatureCancel(string featureName)
        {
            if (!this.IsTrackingEnabled || monitor == null)
            {
                return;
            }

            monitor.TrackFeatureCancel(featureName);
        }

        public void TrackFeatureStart(string featureName)
        {
            if (!this.IsTrackingEnabled || monitor == null)
            {
                return;
            }

            monitor.TrackFeatureStart(featureName);
        }

        public void TrackFeatureStop(string featureName)
        {
            if (!this.IsTrackingEnabled || monitor == null)
            {
                return;
            }

            monitor.TrackFeatureEnd(featureName);
        }

        public void TrackScreenView(string screenView)
        {
            if (!this.IsTrackingEnabled || monitor == null)
            {
                return;
            }

            monitor.TrackScreenView(screenView);
        }

        public void Stop()
        {
            if (monitor != null)
            {
                monitor = null;
            }
        }

        private static void TryInitializeMonitor()
        {
            if (!ConfigurationManager.AppSettings.Keys.OfType<string>().Contains(AnalyticsProductKey))
            {
                return;
            }

            if (!isTrackingEnabled)
            {
                throw new InvalidOperationException("TryInitializeMonitor cannot be called when isTrackingEnabled is false. Set Instance.IsTrackingEnabled to true first.");
            }

            if (monitor != null)
            {
                return;
            }

            string productKey = ConfigurationManager.AppSettings[AnalyticsProductKey];
            if (!string.IsNullOrEmpty(productKey))
            {
                monitor = new GoogleAnalyticsMonitor(productKey, AppName, GetAppVersion());
            }
        }

        private static string GetAppVersion()
        {
            var version = System.Reflection.Assembly.GetAssembly(typeof(Telerik.Windows.Controls.RadControl)).GetName().Version.ToString();
            var source = string.Empty;
#if WPF461
            source = "(Win10 Store)";
#else
            source = ApplicationDeployment.IsNetworkDeployed ? "(Click Once)" : "(MSI)";
#endif
            return version + " " + source;
        }
    }
}