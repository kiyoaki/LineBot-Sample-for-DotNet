using System;
using System.Net.Http;

namespace LineBotNet.Core.ApiClient
{
    public class LineRequestException : Exception
    {
        public override string Message { get; }

        public LineRequestException(HttpResponseMessage response)
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