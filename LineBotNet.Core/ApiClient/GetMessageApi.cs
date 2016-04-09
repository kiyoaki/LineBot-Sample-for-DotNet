using System;
using System.Net.Http;
using System.Threading.Tasks;
using LineBotNet.Core.Data;

namespace LineBotNet.Core.ApiClient
{
    public class GetMessageApi : LineApiClient
    {
        private const string EndpointUrl = "https://trialbot-api.line.me/v1/bot/message/{0}/content";

        public async Task<LineMessageObject> Post(int messageId)
        {
            var uri = new Uri(string.Format(EndpointUrl, messageId));
            return await SendAsync<LineMessageObject>(uri, HttpMethod.Get);
        }
    }
}