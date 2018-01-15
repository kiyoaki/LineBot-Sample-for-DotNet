using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public static void Run(HttpRequestMessage req, TraceWriter log, out string lineBotQueueItem)
{
    string content;
    if (!LineRequest.IsValid(req, out content))
    {
        lineBotQueueItem = null;
        return;
    }

    lineBotQueueItem = content;
}

public static class LineRequest
{
    private const string ChannelSecret = "XXXXXXXXXXXXXXXXXXXXX";

    public static bool IsValid(HttpRequestMessage req, out string content)
    {
        content = null;

        IEnumerable<string> headers;
        if (!req.Headers.TryGetValues("X-Line-Signature", out headers))
        {
            return false;
        }

        var channelSignature = headers.FirstOrDefault();
        if (channelSignature == null)
        {
            return false;
        }

        var secret = Encoding.UTF8.GetBytes(ChannelSecret);
        content = req.Content.ReadAsStringAsync().Result;
        var body = Encoding.UTF8.GetBytes(content);

        using (var hmacsha256 = new HMACSHA256(secret))
        {
            var signature = Convert.ToBase64String(hmacsha256.ComputeHash(body));
            if (channelSignature != signature)
            {
                return false;
            }
        }

        return true;
    }
}