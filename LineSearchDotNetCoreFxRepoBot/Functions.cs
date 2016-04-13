using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LineBotNet.Core.ApiClient;
using LineBotNet.Core.Data;
using LineBotNet.Core.Data.SendingMessageContents;
using LineBotNet.Core.GitHubApi;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace LineSearchDotNetCoreFxRepoBot
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("line-bot-workitems")] string message, TextWriter log)
        {
            log.WriteLine(message);

            var data = JsonConvert.DeserializeObject<LineMessageObject>(message);

            if (data?.Results != null)
            {
                Task.WhenAll(data.Results.Select(lineMessage =>
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

                               //TODO: Use Sending multiple messages, LINE response is {"statusCode":"422","statusMessage":"contentType is not valid : 0"}
                               var sendingMessageApi = new SendMessageApi(log);
                               var result = new GitHubSearchApi(log).Search(lineMessage.TextContent.Text).Result;
                               if (result == null || !result.Any())
                               {
                                   sendingMessage.SetSingleContent(new SendingTextContent("There is no content."));
                                   return Task.WhenAll(sendingMessageApi.Post(sendingMessage));
                               }
                               return Task.WhenAll(result.Where(x => !string.IsNullOrEmpty(x))
                                   .Select(s =>
                                   {
                                       if (s.Length > 1024)
                                       {
                                           s = s.Substring(0, 1024);
                                       }

                                       sendingMessage.SetSingleContent(new SendingTextContent(s));
                                       return sendingMessageApi.Post(sendingMessage);
                                   }));
                           }
                           break;

                       default:
                           log.WriteLine("Not implemented contentType: " + lineMessage.ContentType);
                           break;
                   }

                   return Task.FromResult((SendingMessageResponse[])null);
               })).Wait();
            }
        }
    }
}
