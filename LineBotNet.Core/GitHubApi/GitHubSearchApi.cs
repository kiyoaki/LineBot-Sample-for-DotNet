using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LineBotNet.Core.Configuration;
using LineBotNet.Core.GitHubApi.Data;
using Newtonsoft.Json;

namespace LineBotNet.Core.GitHubApi
{
    public class GitHubSearchApi
    {
        private readonly TextWriter _log;
        public GitHubSearchApi() { }
        public GitHubSearchApi(TextWriter log)
        {
            _log = log;
        }

        private const string SearchUrlBase = "https://api.github.com/search/code?q={0}+in:file+filename:.cs+repo:dotnet/corefx";
        private const string UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0";
        private const int ResultCount = 3;

        public async Task<string[]> Search(string query)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "token " + AppSettings.GitHubAccessToken);
                    httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                    var response = await httpClient.GetAsync(string.Format(SearchUrlBase, query));

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new RequestException(response);
                    }

                    var searchResultJson = await response.Content.ReadAsStringAsync();
                    var searchResult = JsonConvert.DeserializeObject<GitHubSearchResult>(searchResultJson);

                    var detailSearchResultResponse = await Task.WhenAll(searchResult.Items
                        .Take(ResultCount)
                        .OrderBy(_ => Guid.NewGuid())
                        .Select(async resultItem =>
                        {
                            using (var httpClientForTask = new HttpClient())
                            {
                                httpClientForTask.DefaultRequestHeaders.Add("Authorization", "token " + AppSettings.GitHubAccessToken);
                                httpClientForTask.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                                var res = await httpClientForTask.GetAsync(resultItem.Url);
                                return await res.Content.ReadAsStringAsync();
                            }
                        }));

                    var isCallLimitExceeded = false;
                    return detailSearchResultResponse.Select(json =>
                    {
                        _log?.WriteLine("DetailItem: " + json);

                        var item = JsonConvert.DeserializeObject<GitHubSearchDetailItem>(json);
                        if (string.IsNullOrEmpty(item.Content))
                        {
                            return null;
                        }

                        if (isCallLimitExceeded)
                        {
                            return null;
                        }

                        if (item.IsCallLimitExceeded)
                        {
                            isCallLimitExceeded = true;
                            return "Search limit exceeded. Please try later.";
                        }

                        return string.Join(Environment.NewLine,
                            $"FileName: {item.Name}",
                            $"URL: {item.HtmlUrl}",
                            "",
                            $"{Encoding.UTF8.GetString(Convert.FromBase64String(item.Content))}");
                    })
                    .Where(x => x != null)
                    .ToArray();
                }
            }
            catch (Exception ex)
            {
                _log?.WriteLine(string.Join(Environment.NewLine,
                            $"Message: {ex.Message}",
                            $"Source: {ex.Source}",
                            "",
                            $"StackTrace: {ex.StackTrace}"));

                return new[] { "Error has occuerd." };
            }
        }
    }
}
