using System;
using System.Net.Http;
using System.Threading.Tasks;
using LineBotNet.Core.Configuration;
using Newtonsoft.Json;

namespace LineBotNet.Core.ApiClient
{
    public class LineApiClient
    {
        protected async Task SendAsync(Uri uri, HttpMethod method, string json = null)
        {
            using (var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(5)
            })
            {
                using (var requestMessage = new HttpRequestMessage
                {
                    Method = method,
                    RequestUri = uri,
                    Headers =
                        {
                            { "Content-Type", "application/json; charser=UTF-8" },
                            { "X-Line-ChannelID", AppSettings.LineChannelId },
                            { "X-Line-ChannelSecret", AppSettings.LineChannelSecret },
                            { "X-Line-Trusted-User-With-ACL", AppSettings.LineTrustedUserWithAcl }
                        }
                })
                {
                    if (json != null)
                    {
                        requestMessage.Content = new StringContent(json);
                    }

                    var result = await httpClient.SendAsync(requestMessage);
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new LineRequestException(result.StatusCode, string.Join(Environment.NewLine,
                            "Headers: " + result.Headers));
                    }
                }
            }
        }

        protected async Task<T> SendAsync<T>(Uri uri, HttpMethod method, string json = null)
        {
            using (var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(5)
            })
            {
                using (var requestMessage = new HttpRequestMessage
                {
                    Method = method,
                    RequestUri = uri,
                    Headers =
                        {
                            { "Content-Type", "application/json; charser=UTF-8" },
                            { "X-Line-ChannelID", AppSettings.LineChannelId },
                            { "X-Line-ChannelSecret", AppSettings.LineChannelSecret },
                            { "X-Line-Trusted-User-With-ACL", AppSettings.LineTrustedUserWithAcl }
                        }
                })
                {
                    if (json != null)
                    {
                        requestMessage.Content = new StringContent(json);
                    }

                    var result = await httpClient.SendAsync(requestMessage);
                    var responseContent = await result.Content.ReadAsStringAsync();

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new LineRequestException(result.StatusCode, string.Join(Environment.NewLine,
                            "Headers: " + result.Headers,
                            "Content: " + responseContent));
                    }

                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
        }
    }
}