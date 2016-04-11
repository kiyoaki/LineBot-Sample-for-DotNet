using System;
using System.Collections.Generic;
using LineBotNet.Core.Data.SendingMessageContents;
using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{
    public class LineMessageObject
    {
        [JsonProperty("result")]
        public LineMessage[] Results { get; set; }
    }

    public class LineMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("fromChannel")]
        public long FromChannel { get; set; }

        [JsonProperty("to")]
        public string[] To { get; set; }

        [JsonProperty("toChannel")]
        public long ToChannel { get; set; }

        [JsonProperty("createdTime")]
        public long CreatedTime { get; set; }

        [JsonProperty("content")]
        public Dictionary<string, object> Content { get; set; }

        [JsonIgnore]
        public ContentType ContentType
        {
            get
            {
                if (Content == null || !Content.ContainsKey("contentType"))
                {
                    return ContentType.UnKnown;
                }

                ContentType contentType;
                if (Enum.TryParse(Convert.ToString(Content["contentType"]), out contentType))
                {
                    return contentType;
                }

                return ContentType.UnKnown;
            }
        }

        [JsonIgnore]
        public ReceivingTextContent TextContent => ConvertContent<ReceivingTextContent>();

        private T ConvertContent<T>() where T : class
        {
            var json = JsonConvert.SerializeObject(Content);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}