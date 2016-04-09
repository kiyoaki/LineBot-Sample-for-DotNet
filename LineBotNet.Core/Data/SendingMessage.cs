using System;
using System.Collections.Generic;
using LineBotNet.Core.Data.SendingMessageContents;
using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{
    public class SendingMessage
    {
        [JsonProperty("to")]
        public string[] To { get; set; }

        [JsonProperty("toChannel")]
        public int ToChannel => 1383378250;

        [JsonProperty("eventType")]
        public long EventType => 138311608800106203;

        [JsonProperty("content")]
        public Dictionary<string, object> Content { get; set; }

        public void SetSingleContent(SendingMessageContent content)
        {
            Content = content.Create();
        }

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
    }
}