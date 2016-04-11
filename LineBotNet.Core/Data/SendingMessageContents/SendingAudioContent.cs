using System;
using System.Collections.Generic;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public class SendingAudioContent : SendingMessageContent
    {
        private readonly string _originalContentUrl;
        private readonly int _audioLengthMilliseconds;

        public SendingAudioContent(string originalContentUrl, int audioLengthMilliseconds)
        {
            if (originalContentUrl == null)
            {
                throw new ArgumentNullException(nameof(originalContentUrl));
            }

            _originalContentUrl = originalContentUrl;
            _audioLengthMilliseconds = audioLengthMilliseconds;
        }

        public override ContentType ContentType => ContentType.Audio;

        public override Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                ["contentType"] = (int)ContentType.Audio,
                ["originalContentUrl"] = _originalContentUrl,
                ["contentMetada"] = new Dictionary<string, string>
                {
                    ["AUDLEN"] = _audioLengthMilliseconds.ToString()
                }
            };
        }
    }
}