namespace LineBotNet.Core.Configuration
{
    public class AppSettings : AppSettingsBase
    {
        protected AppSettings() { }

        public static string MsTranslateApiClientId => Setting("MsTranslateApiClientId");

        public static string MsTranslateApiClientSecret => Setting("MsTranslateApiClientSecret");

        public static string GitHubAccessToken => Setting("GitHubAccessToken");
    }
}