using LineBotNet.Core;
using LineMessaging;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace LineTranslateBot
{
    public class Functions
    {
        private static readonly LineOAuthClient oAuthClient =
            new LineOAuthClient(ConfigurationManager.AppSettings["ChannelId"], ConfigurationManager.AppSettings["ChannelSecret"]);

        private static LineOAuthTokenResponse tokenResponse;

        public static async Task ProcessQueueMessage([QueueTrigger("line-bot-workitems")] string message, TextWriter log)
        {
            log.WriteLine(message);

            var data = JsonConvert.DeserializeObject<LineWebhookContent>(message);

            if (data?.Events != null)
            {
                foreach (var webhookEvent in data.Events)
                {
                    log.WriteLine("event type: " + webhookEvent.Type);
                    switch (webhookEvent.Type)
                    {
                        case WebhookRequestEventType.Message:
                            if (webhookEvent.Message.Type == MessageType.Text)
                            {
                                log.WriteLine("text: " + webhookEvent.Message.Text);

                                if (tokenResponse == null || tokenResponse.ExpiresIn < DateTime.Now.AddDays(-1))
                                {
                                    tokenResponse = await oAuthClient.GetAccessToken();
                                }

                                var client = new LineMessagingClient(tokenResponse.AccessToken);

                                var translateApi = new TranslateApi();
                                var translated = translateApi.Translate(webhookEvent.Message.Text);
                                if (string.IsNullOrEmpty(translated))
                                {
                                    throw new ApplicationException("reply message is empty");
                                }

                                await client.PushMessage(webhookEvent.Source.UserId, translated);
                            }
                            break;

                        default:
                            log.WriteLine("Not implemented event type: " + webhookEvent.Type);
                            break;
                    }
                }
            }
        }
    }
}
