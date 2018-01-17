using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public static async Task<string> Run(HttpRequestMessage req, TraceWriter log)
{
    IEnumerable<string> headers;
    if (!req.Headers.TryGetValues("X-Line-Signature", out headers))
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
        log.Warning("Please set ChannelSecret in App Settings");
        return null;
    }

    var secret = Encoding.UTF8.GetBytes(LineSettings.ChannelSecret);
    var content = await req.Content.ReadAsStringAsync();
    var body = Encoding.UTF8.GetBytes(content);

    using (var hmacsha256 = new HMACSHA256(secret))
    {
        var signature = Convert.ToBase64String(hmacsha256.ComputeHash(body));
        if (channelSignature != signature)
        {
            return null;
        }
    }

    return content;
}

internal static class LineSettings
{
    internal static readonly string ChannelSecret = Environment.GetEnvironmentVariable("ChannelSecret", EnvironmentVariableTarget.Process);
}