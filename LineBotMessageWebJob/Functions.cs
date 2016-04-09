using System.IO;
using LineBotNet.Core.Data;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace LineBotMessageWebJob
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("line-bot-workitems")] string message, TextWriter log)
        {
            log.WriteLine("==================== job start ====================");

            log.WriteLine(message);

            var data = JsonConvert.DeserializeObject<LineMessageObject>(message);
            if (data?.Results != null)
            {
                foreach (var lineMessage in data.Results)
                {
                    log.WriteLine("ContentType: " + lineMessage.ContentType);
                    var converted = lineMessage.ConvertContent<ReceivingMessageContent>();
                    if (converted != null)
                    {
                        log.WriteLine("text: " + converted.Text);
                    }
                }
            }

            log.WriteLine("==================== job end ====================");
        }
    }
}
