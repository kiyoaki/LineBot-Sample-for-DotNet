using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LineBotMessageCollector
{
    public static class Function1
    {
        [FunctionName("CollectMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            StringValues headers;
            if (!req.Headers.TryGetValue("X-Line-Signature", out headers))
            {
                return null;
            }

            var channelSignature = headers.FirstOrDefault();
            if (channelSignature == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(LineSettings.ChannelSecret))
            {
                log.LogWarning("Please set ChannelSecret in App Settings");
                return null;
            }

            var secret = Encoding.UTF8.GetBytes(LineSettings.ChannelSecret);
            var content = await req.ReadAsStringAsync();
            var body = Encoding.UTF8.GetBytes(content);

            using (var hmacsha256 = new HMACSHA256(secret))
            {
                var signature = Convert.ToBase64String(hmacsha256.ComputeHash(body));
                if (channelSignature != signature)
                {
                    return null;
                }
            }

            return new OkObjectResult(content);
        }
    }
}

internal static class LineSettings
{
    internal static readonly string ChannelSecret = Environment.GetEnvironmentVariable("ChannelSecret", EnvironmentVariableTarget.Process);
}
