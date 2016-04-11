using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LineBotNet.Core.Data;
using Newtonsoft.Json;

namespace LineBotNet.Core.ApiClient
{
    public class SendMessageApi : LineApiClient
    {
        public SendMessageApi() { }

        public SendMessageApi(TextWriter log) : base(log) { }

        private const string EndpointUrl = "https://trialbot-api.line.me/v1/events";

        public async Task<SendingMessageResponse> Post(SendingMessage message)
        {
            if (message?.Content == null || !message.Content.Any())
            {
                return null;
            }

            var uri = new Uri(EndpointUrl);
            var body = JsonConvert.SerializeObject(message);
            return await SendAsync<SendingMessageResponse>(uri, HttpMethod.Post, body);
        }
    }
}