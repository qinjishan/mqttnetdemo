using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.rtc.Model.V20180111;
using RtcWebDemo.Models;

namespace RtcWebDemo.AliyunHelper
{
    public class RtcHelper
    {



       public  static async Task<ChannelAuth> RecoverForError(Exception ex, string appId, string channelId)
        {
            bool fatal = false;
            string requestId = "";

            ClientException cex = ex as ClientException;
            if (cex != null && cex.ErrorCode != null)
            {
                requestId = cex.RequestId;
                string code = cex.ErrorCode;
                if (code == "IllegalOperationApp")
                {
                    fatal = true;
                }
                else if (code.StartsWith("InvalidAccessKeyId", StringComparison.Ordinal))
                {
                    fatal = true;
                }
                else if (code == "SignatureDoesNotMatch")
                {
                    fatal = true;
                }
            }

            if (fatal)
            {
                throw ex;
            }

            string recovered = "RCV-" + Guid.NewGuid().ToString();
            System.Console.WriteLine("Recover from {0}, recovered={1}", ex.ToString(), recovered);

            ChannelAuth auth = new ChannelAuth();
            auth.AppId = appId;
            auth.ChannelId = channelId;
            auth.Nonce = recovered;
            auth.Timestamp = 0;
            auth.ChannelKey = recovered;
            auth.Recovered = true;

            return auth;
        }

       public static async Task<ChannelAuth> CreateChannel(
            string appId, string channelId,
            string regionId, string endpoint, string accessKeyId,
            string accessKeySecret)
        {
            try
            {
                IClientProfile profile = DefaultProfile.GetProfile(
                    regionId, accessKeyId, accessKeySecret);
                IAcsClient client = new DefaultAcsClient(profile);

                CreateChannelRequest request = new CreateChannelRequest();
                request.AppId = appId;
                request.ChannelId = channelId;

                // Strongly recomment to set the RTC endpoint,
                // because the exception is not the "right" one if not set.
                // For example, if access-key-id is invalid:
                //      1. if endpoint is set, exception is InvalidAccessKeyId.NotFound
                //      2. if endpoint isn't set, exception is SDK.InvalidRegionId
                // that's caused by query endpoint failed.
                // @remark SDk will cache endpoints, however it will query endpoint for the first
                //      time, so it's good for performance to set the endpoint.
                DefaultProfile.AddEndpoint(regionId, regionId, request.Product, endpoint);

                // Use HTTP, x3 times faster than HTTPS.
                request.Protocol = Aliyun.Acs.Core.Http.ProtocolType.HTTP;

                CreateChannelResponse response = client.GetAcsResponse(request);

                ChannelAuth auth = new ChannelAuth();
                auth.AppId = appId;
                auth.ChannelId = channelId;
                auth.Nonce = response.Nonce;
                auth.Timestamp = (Int64)response.Timestamp;
                auth.ChannelKey = response.ChannelKey;
                auth.Recovered = false;
                auth.RequestId = response.RequestId;

                return auth;
            }
            catch (Exception ex)
            {
                return await RecoverForError(ex, appId, channelId);
            }
        }

       

       public static async Task<string> CreateToken(
            string channelId, string channelKey, string appid, string userId,
            string nonce, Int64 timestamp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(channelId).Append(channelKey);
            sb.Append(appid).Append(userId);
            sb.Append(nonce).Append(timestamp);

            using (SHA256 hash = SHA256.Create())
            {
                byte[] checksum = hash.ComputeHash(
                    Encoding.ASCII.GetBytes(sb.ToString()));

                string token = HexEncode(checksum);
                return token;
            }
        }

       private static string HexEncode(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

    }
}
