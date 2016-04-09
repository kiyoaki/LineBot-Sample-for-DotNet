using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{
    public class UserProfile
    {
        [JsonProperty("contacts")]
        public Contact[] Contacts { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("display")]
        public int Display { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}