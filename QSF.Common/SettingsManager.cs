using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace Telerik.Windows.Controls.QuickStart
{
	public class SettingsManager
	{
		private static readonly SettingsManager instance = new SettingsManager();

		private bool isAnalyticsTrackingEnabled;

		private SettingsManager()
		{
			this.ReadPersistedSettings();
		}

		public static SettingsManager Instance
		{
			get
			{
				return instance;
			}
		}

		public bool IsAnalyticsTrackingEnabled
		{
			get
			{
				return this.isAnalyticsTrackingEnabled;
			}
			set
			{
				this.isAnalyticsTrackingEnabled = value;
				this.SaveIsAnalayticsTrackingEnabledValue(value);
			}
		}

		private void ReadPersistedSettings()
		{
			this.isAnalyticsTrackingEnabled = Properties.Settings.Default.IsAnalyticsTrackingEnabled;
		}

		private void SaveIsAnalayticsTrackingEnabledValue(bool value)
		{
			Properties.Settings.Default.IsAnalyticsTrackingEnabled = value;
			Properties.Settings.Default.Save();
		}
	}
}