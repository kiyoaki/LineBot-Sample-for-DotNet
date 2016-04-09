using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LineBotNet.Core.Data;

namespace LineBotNet.Core.ApiClient
{
    public class UserProfileApi : LineApiClient
    {
        private const string EndpointUrl = "https://trialbot-api.line.me/v1/profiles";

        public async Task<UserProfile> Get(params string[] mids)
        {
            if (mids == null || !mids.Any())
            {
                throw new ArgumentException("mids is required");
            }

            var queryParamter = "?mids=" + string.Join(",", mids);
            var uri = new Uri(EndpointUrl + queryParamter);
            return await SendAsync<UserProfile>(uri, HttpMethod.Get);
        }
    }
}