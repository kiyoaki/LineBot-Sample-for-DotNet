using System;
using System.Collections.Generic;

namespace LineBotNet.Core.Data.SendingMessageContents
{
    public class LocationContent : SendingMessageContent
    {
        private readonly string _text;
        private readonly Location _location;

        public LocationContent(string text, Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            _text = text;
            _location = location;
        }

        public override ContentType ContentType => ContentType.Location;

        public override Dictionary<string, object> Create()
        {
            return new Dictionary<string, object>
            {
                ["contentType"] = (int)ContentType.Location,
                ["text"] = _text,
                ["location"] = new Dictionary<string, object>
                {
                    ["title"] = _location.Title,
                    ["latitude"] = _location.Latitude,
                    ["longitude"] = _location.Longitude
                }
            };
        }
    }
}