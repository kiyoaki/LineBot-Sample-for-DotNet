using Newtonsoft.Json;

namespace LineBotNet.Core.GitHubApi.Data
{
    public class GitHubSearchResult
    {
        [JsonProperty("items")]
        public GitHubSearchResultItem[] Items { get; set; }
    }

    public class GitHubSearchResultItem
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class GitHubSearchDetailItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonIgnore]
        public bool IsCallLimitExceeded => !string.IsNullOrEmpty(Message) && Message.StartsWith("API rate limit exceeded");
    }
}
