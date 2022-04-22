namespace Telerik.Windows.Controls.QuickStart.Analytics
{
    public static class AnalyticsConstants
    {
        //features to track
        public const string Theming = "Theme"; // expected to be appended to, like "Theme.Fluent_Dark"
        public const string ApplicationCrash = "Application.Crash";
        public const string NavigateBySearch = "Navigate.BySearch"; // expected to be appended to, like "Navigate.BySearch.SomeExample"
        public const string Navigate = "Navigate"; // expected to be appended to, like "Navigate.ToSomeExample"
        public const string SwitchMode = "SwitchMode"; // expected to be appended to: "SwitchMode.Touch.SomePage" or "SwitchMode.Desktop.SomePage"

        //features to get time
        public const string ApplicationStartupTime = "Application.StartUpTime"; // the time between the app launch and the home view loaded
        public const string ApplicationUptime = "Application.UpTime";
    }
}