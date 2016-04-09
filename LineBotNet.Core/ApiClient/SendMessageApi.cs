using System;
using System.Net.Http;
using System.Threading.Tasks;
using LineBotNet.Core.Data;
using Newtonsoft.Json;

namespace LineBotNet.Core.ApiClient
{
    public class SendMessageApi : LineApiClient
    {
        private const string EndpointUrl = "https://trialbot-api.line.me/v1/events";

        public async Task Post(SendingMessage message)
        {
            var uri = new Uri(EndpointUrl);
            var body = JsonConvert.SerializeObject(message);
            await SendAsync(uri, HttpMethod.Post, body);
        }
    }
}