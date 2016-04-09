using System.Collections.Generic;
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

        [JsonProperty("to")]
        public string[] To { get; set; }

        [JsonProperty("content")]
        public Dictionary<string, object> Content { get; set; }

        [JsonIgnore]
        public ContentType ContentType
        {
            get
            {
                if (Content == null || Content.ContainsKey("contentType"))
                {
                    return ContentType.UnKnown;
                }

                return (ContentType)Content["contentType"];
            }
        }

        public T ConvertContent<T>() where T : class
        {
            var json = JsonConvert.SerializeObject(Content);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}