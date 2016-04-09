using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{
    public class Contact
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }
    }
}