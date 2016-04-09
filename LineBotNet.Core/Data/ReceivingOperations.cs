using Newtonsoft.Json;

namespace LineBotNet.Core.Data
{

    public class ReceivingOperationContent
    {
        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("opType")]
        public OpType OpType { get; set; }

        [JsonProperty("params")]
        public string[] Params { get; set; }


    }

    public enum OpType
    {
        AddedAsfriend = 4,
        BlockedAccount = 8,
    }
}