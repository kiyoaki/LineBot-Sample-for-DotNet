namespace LineBotNet.Core.Configuration
{
    public class AppSettings : AppSettingsBase
    {
        protected AppSettings() { }

        public static string LineChannelId => Setting("LineChannelId");

        public static string LineChannelSecret => Setting("LineChannelSecret");

        public static string LineTrustedUserWithAcl => Setting("LineTrustedUserWithAcl");

        public static string MsTranslateApiClientId => Setting("MsTranslateApiClientId");

        public static string MsTranslateApiClientSecret => Setting("MsTranslateApiClientSecret");

        public static string GitHubAccessToken => Setting("GitHubAccessToken");
    }
}