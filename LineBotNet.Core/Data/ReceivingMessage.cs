using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{
    public class ReceivingMessageContent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("contentType")]
        public ContentType ContentType { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("createdTime")]
        public long CreatedTime { get; set; }

        [JsonProperty("contentMetadata")]
        public ContentMetadata ContentMetadata { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Location
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class ContentMetadata
    {
        [JsonProperty("STKPKGID")]
        public string StickerPackageId { get; set; }

        [JsonProperty("STKID")]
        public string StickerId { get; set; }

        [JsonProperty("STKVER")]
        public string StickerVersion { get; set; }

        [JsonProperty("STKTXT")]
        public string StickerText { get; set; }

        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }

    public enum ContentType
    {
        UnKnown = 0,
        Text = 1,
        Image = 2,
        Video = 3,
        Audio = 4,
        Location = 7,
        Sticker = 8,
        Contact = 10,
        RichMessage = 12,
    }
}