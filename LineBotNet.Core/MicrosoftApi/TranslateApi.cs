using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Xml;
using LineBotNet.Core.MicrosoftApi;

namespace LineBotNet.Core
{
    public class TranslateApi
    {
        public string Translate(string text, string from = "en", string to = "ja")
        {
            using (var httpClient = new HttpClient())
            {
                var uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AdmAuthentication.Instance.AccessToken.Token);
                var response = httpClient.GetAsync(uri).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new RequestException(response);
                }

                using (var result = response.Content.ReadAsStreamAsync().Result)
                {
                    using (var reader = XmlReader.Create(result))
                    {
                        return reader.ReadElementString();
                    }

                }
            }
        }
    }
}
