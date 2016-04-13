using System;
using System.Collections.Generic;
using System.Linq;
using LineBotNet.Core.Data.SendingMessageContents;
using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{
    public class SendingMessage
    {
        [JsonProperty("to")]
        public List<string> To { get; set; }

        [JsonProperty("toChannel")]
        public int ToChannel => 1383378250;

        [JsonProperty("eventType")]
        public string EventType => "138311608800106203";

        [JsonProperty("content")]
        public Dictionary<string, object> Content { get; set; }

        public void SetSingleContent(SendingMessageContent content)
        {
            Content = content.Create();
        }

        public void AddTo(string mid)
        {
            if (To == null)
            {
                To = new List<string>();
            }

            if (!To.Contains(mid))
            {
                To.Add(mid);
            }
        }

        public void AddTo(string[] mids)
        {
            if (To == null)
            {
                To = new List<string>();
            }

            foreach (var mid in mids.Where(x => !To.Contains(x)))
            {
                To.Add(mid);
            }
        }

        /* TODO: Use this method, LINE response is {"statusCode":"422","statusMessage":"contentType is not valid : 0"}
        public void AddMultipleContent(SendingMessageContent content)
        {
            if (Content == null)
            {
                Content = new Dictionary<string, object>
                {
                    ["messageNotified"] = 0,
                    ["messages"] = new List<Dictionary<string, object>>()
                };
            }

            var list = Content["messages"] as List<Dictionary<string, object>>;
            if (list == null)
            {
                throw new InvalidOperationException("single content is already added.");
            }

            list.Add(content.Create());
        }
        */
    }

    public class SendingMessageResponse
    {
        [JsonProperty("failed")]
        public string[] Failed { get; set; }

        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("version")]
        public double Version { get; set; }
    }
}