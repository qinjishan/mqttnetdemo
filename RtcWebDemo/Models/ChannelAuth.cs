using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RtcWebDemo.Models
{
    public class ChannelAuth
    {
        public string AppId;
        public string ChannelId;
        public string Nonce;
        public Int64 Timestamp;
        public string ChannelKey;
        public bool Recovered;
        public string RequestId;
    }
}
