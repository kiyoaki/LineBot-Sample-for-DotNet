using LineBotNet.Core.ApiClient;
using LineBotNet.Core.Data;
using LineBotNet.Core.Data.SendingMessageContents;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LineBotMessageWebJob
{
    public class Functions
    {
        public async static Task ProcessQueueMessage([QueueTrigger("line-bot-workitems")] string message, TextWriter log)
        {
            log.WriteLine(message);

            var data = JsonConvert.DeserializeObject<LineMessageObject>(message);

            if (data?.Results != null)
            {
                foreach (var lineMessage in data.Results)
                {
                    if (lineMessage.Content != null)
                    {
                        log.WriteLine("Content: " + string.Join(Environment.NewLine,
                            lineMessage.Content.Select(x => $"{x.Key}={x.Value}")));
                    }

                    log.WriteLine("ContentType: " + lineMessage.ContentType);
                    switch (lineMessage.ContentType)
                    {
                        case ContentType.Text:
                            if (lineMessage.TextContent != null)
                            {
                                log.WriteLine("text: " + lineMessage.TextContent.Text);

                                var sendingMessage = new SendingMessage();

                                sendingMessage.AddTo(lineMessage.TextContent.From);
                                sendingMessage.SetSingleContent(new SendingTextContent(lineMessage.TextContent.Text));

                                await new SendMessageApi(log).Post(sendingMessage);
                            }
                            break;

                        default:
                            log.WriteLine("Not implemented contentType: " + lineMessage.ContentType);
                            break;
                    }
                }
            }
        }
    }
}
