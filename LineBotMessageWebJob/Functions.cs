using LineMessaging;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace LineBotMessageWebJob
{
    public class Functions
    {
        private static readonly LineOAuthClient oAuthClient =
            new LineOAuthClient(ConfigurationManager.AppSettings["ChannelId"], ConfigurationManager.AppSettings["ChannelSecret"]);

        private static LineOAuthTokenResponse tokenResponse;

        public static async Task ProcessQueueMessage([QueueTrigger("line-bot-workitems")] string message, TextWriter log)
        {
            log.WriteLine(message);
            LineWebhookContent data;
            try
            {
                data = JsonConvert.DeserializeObject<LineWebhookContent>(message);
            }
            catch (Exception ex)
            {
                log.WriteLine("Ignore deserialization error: " + ex.ToString());
                return;
            }

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
                                await client.PushMessage(webhookEvent.Source.UserId, webhookEvent.Message.Text);
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
