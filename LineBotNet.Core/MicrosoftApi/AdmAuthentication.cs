using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LineBotNet.Core.Configuration;
using Newtonsoft.Json;

namespace LineBotNet.Core.MicrosoftApi
{
    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private const string ScopeUrl = "http://api.microsofttranslator.com";
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly Timer _accessTokenRenewer;

        private const int RefreshTokenDuration = 9;

        private static volatile AdmAuthentication _instance;
        private static readonly object SyncRoot = new object();

        private AdmAuthentication()
        {
            _clientId = AppSettings.MsTranslateApiClientId;
            _clientSecret = AppSettings.MsTranslateApiClientSecret;
            AccessToken = GetToken().Result;
            _accessTokenRenewer = new Timer(OnTokenExpiredCallback, this, TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
        }

        public static AdmAuthentication Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new AdmAuthentication();
                    }
                }

                return _instance;
            }
        }

        public AdmAccessToken AccessToken { get; private set; }

        private void RenewAccessToken()
        {
            AccessToken = GetToken().Result;
            Console.WriteLine($"Renewed token for user: {_clientId} is: {AccessToken.Token}");
        }

        private async Task<AdmAccessToken> GetToken()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(DatamarketAccessUri,
                    new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["grant_type"] = "client_credentials",
                        ["client_id"] = _clientId,
                        ["client_secret"] = _clientSecret,
                        ["scope"] = ScopeUrl
                    }));

                if (!response.IsSuccessStatusCode)
                {
                    throw new RequestException(response);
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AdmAccessToken>(responseBody);
            }
        }

        private void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                RenewAccessToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed renewing access token. Details: {ex.Message}");
            }
            finally
            {
                try
                {
                    _accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to reschedule the timer to renew access token. Details: {ex.Message}");
                }
            }
        }
    }
}
