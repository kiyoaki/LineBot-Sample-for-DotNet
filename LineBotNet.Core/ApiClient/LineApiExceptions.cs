using System;
using System.Collections.Generic;
using System.Net;

namespace LineBotNet.Core.ApiClient
{
    public class LineRequestException : Exception
    {
        public static readonly Dictionary<HttpStatusCode, string> StatusMessages = new Dictionary<HttpStatusCode, string>
        {
            { HttpStatusCode.OK, "正常に処理が完了しました。" },
            { HttpStatusCode.Created, "新しいリソースの生成が成功しました。" },
            { HttpStatusCode.Accepted, "リクエストが正常に受け付けられました。" },
            { HttpStatusCode.BadRequest, "リクエストデータが不正です。" },
            { HttpStatusCode.Unauthorized, "OAuth による認可が失敗しています。" },
            { HttpStatusCode.Forbidden, "認証エラー以外の理由でアクセス出来ない場合です。" },
            { HttpStatusCode.NotFound, "存在しないリソースです。" },
            { HttpStatusCode.MethodNotAllowed, "指定された操作が許可されていません。" },
            { HttpStatusCode.Conflict, "指定された操作がリソースの現在の状態と競合したため処理が失敗しました。" },
            { HttpStatusCode.InternalServerError, "OpenSocial API側の問題によるエラーです。" },
            { HttpStatusCode.NotImplemented, "指定された操作はOpenSocial API側に実装されていません。" },
            { HttpStatusCode.ServiceUnavailable, "一時的にOpenSocial API側にアクセスが出来ません。" }
        };

        public override string Message { get; }

        public LineRequestException(string message)
        {
            Message = message;
        }

        public LineRequestException(HttpStatusCode statusCode, string message = "")
        {
            string statusMessage;
            if (StatusMessages.TryGetValue(statusCode, out statusMessage))
            {
                Message = statusMessage + message;
            }
            else
            {
                Message = message;
            }
        }
    }
}