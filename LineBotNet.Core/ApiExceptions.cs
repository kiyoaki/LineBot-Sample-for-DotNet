using System;
using System.Net.Http;

namespace LineBotNet.Core
{
    public class RequestException : Exception
    {
        public override string Message { get; }

        public RequestException(HttpResponseMessage response)
        {
            var statusCode = (int)response.StatusCode;
            var reasonPhrase = response.ReasonPhrase;
            var content = response.Content.ReadAsStringAsync().Result;

            Message = string.Join(Environment.NewLine,
                $"StatusCode: {statusCode}",
                $"ReasonPhrase: {reasonPhrase}",
                $"Content: {content}");
        }
    }
}