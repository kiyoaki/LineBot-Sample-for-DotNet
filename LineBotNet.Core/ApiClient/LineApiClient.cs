using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LineBotNet.Core.Configuration;
using Newtonsoft.Json;

namespace LineBotNet.Core.ApiClient
{
    public abstract class LineApiClient
    {
        private readonly TextWriter _log;
        protected LineApiClient() { }
        protected LineApiClient(TextWriter log)
        {
            _log = log;
        }

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
                    RequestUri = uri
                })
                {
                    SetLineApiHeaders(requestMessage);

                    if (json != null)
                    {
                        requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    Logging(requestMessage.Headers, json);

                    var result = await httpClient.SendAsync(requestMessage);
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new LineRequestException(result);
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
                    RequestUri = uri
                })
                {
                    SetLineApiHeaders(requestMessage);

                    if (json != null)
                    {
                        requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    Logging(requestMessage.Headers, json);

                    var result = await httpClient.SendAsync(requestMessage);
                    var responseContent = await result.Content.ReadAsStringAsync();

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new LineRequestException(result);
                    }

                    var response = JsonConvert.DeserializeObject<T>(responseContent);

                    _log?.WriteLine("Response: " + response);

                    return response;
                }
            }
        }

        private void Logging(HttpRequestHeaders headers, string content)
        {
            if (_log == null) return;

            _log.WriteLine("Request Header: " + string.Join(", ",
                headers.ToDictionary(x => x.Key, x => x.Value.FirstOrDefault())));
            _log.WriteLine("Request Content: " + content);
        }

        private static void SetLineApiHeaders(HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.Add("X-Line-ChannelID", AppSettings.LineChannelId);
            requestMessage.Headers.Add("X-Line-ChannelSecret", AppSettings.LineChannelSecret);
            requestMessage.Headers.Add("X-Line-Trusted-User-With-ACL", AppSettings.LineTrustedUserWithAcl);
        }
    }
}