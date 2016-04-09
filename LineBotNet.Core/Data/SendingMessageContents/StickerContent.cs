using System.Collections.Generic;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public class StickerContent : SendingMessageContent
    {
        private readonly int _stickerPackageId;
        private readonly int _stickerId;
        private readonly int _stickerVersion;

        public StickerContent(int stickerPackageId, int stickerId, int stickerVersion)
        {
            _stickerPackageId = stickerPackageId;
            _stickerId = stickerId;
            _stickerVersion = stickerVersion;
        }

        public override ContentType ContentType => ContentType.Sticker;

        public override Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                ["contentType"] = (int)ContentType.Sticker,
                ["toType"] = 1,
                ["contentMetadata"] = new Dictionary<string, object>
                {
                    ["STKPKGID"] = _stickerPackageId,
                    ["STKID"] = _stickerId,
                    ["STKVER"] = _stickerVersion
                }
            };
        }
    }
}