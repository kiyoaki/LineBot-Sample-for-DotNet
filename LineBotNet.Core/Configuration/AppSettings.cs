namespace LineBotNet.Core.Configuration
{
    public class AppSettings : AppSettingsBase
    {
        protected AppSettings() { }

        public static string LineChannelId => Setting("LineChannelId");

        public static string LineChannelSecret => Setting("LineChannelSecret");

        public static string LineTrustedUserWithAcl => Setting("LineTrustedUserWithAcl");
    }
}