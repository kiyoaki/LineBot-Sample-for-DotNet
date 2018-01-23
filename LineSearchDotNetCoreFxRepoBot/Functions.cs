using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LineBotNet.Core.GitHubApi;
using LineMessaging;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace LineSearchDotNetCoreFxRepoBot
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

                                var result = await new GitHubSearchApi(log).Search(webhookEvent.Message.Text);
                                if (result == null || !result.Any())
                                {
                                    await client.PushMessage(webhookEvent.Source.UserId, "There is no content.");
                                    return;
                                }

                                foreach (var s in result.Where(x => !string.IsNullOrEmpty(x)))
                                {
                                    if (s.Length > 1024)
                                    {
                                        await client.PushMessage(webhookEvent.Source.UserId, s.Substring(0, 1024));
                                    }
                                    else
                                    {
                                        await client.PushMessage(webhookEvent.Source.UserId, s);
                                    }
                                }
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
