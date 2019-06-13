using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.rtc.Model.V20180111;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RtcWebDemo.AliyunHelper;
using RtcWebDemo.Models;

namespace RtcWebDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private const string appId = "ajiucud6";
        private const  string regionId = "cn-hangzhou";
        private const string endpoint= "rtc.aliyuncs.com";
        private const string accessKeyId = "LTAIx323abVewUZz";
        private const string accessKeySecret = "zYv085rn0N9tvJmF8aHjDuRcPZlcWU";
        private const string gslb = "https://rgslb.rtc.aliyuncs.com";

        static Dictionary<string, ChannelAuth> channels = new Dictionary<string, ChannelAuth>();

        

        private async Task<ChannelAuth> RecoverForError(Exception ex, string appId, string channelId)
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




        [HttpGet(nameof(GetToken))]
        public async Task<IActionResult> GetToken()
        {
            string channelId = Request.Query["room"];
            string user = Request.Query["user"];
            string channelUrl = string.Format("{0}/{1}", appId, channelId);

            DateTime starttime = DateTime.Now;

            ChannelAuth auth = null;
            using (Mutex locker = new Mutex())
            {
                locker.WaitOne();

                if (channels.ContainsKey(channelUrl))
                {
                    auth = channels[channelUrl];
                }
                else
                {
                    auth =await RtcHelper.CreateChannel(appId, channelId, regionId, endpoint, accessKeyId, accessKeySecret);

                    // If recovered from error, we should never cache it,
                    // and we should try to request again next time.
                    if (!auth.Recovered)
                    {
                        channels[channelUrl] = auth;
                    }

                    //System.Console.WriteLine(String.Format(
                    //    "CreateChannel requestId={4}, cost={6}ms, channelId={0}, nonce={1}, timestamp={2}, channelKey={3}, recovered={5}",
                    //    channelId, auth.Nonce, auth.Timestamp, auth.ChannelKey, auth.RequestId, auth.Recovered, DateTime.Now.Subtract(starttime).Milliseconds));
                }
            }


            string userId = Request.Query["userId"];
            string token =await RtcHelper.CreateToken(channelId, auth.ChannelKey, appId, userId,auth.Nonce, auth.Timestamp);
            string username = String.Format(
                "{0}?appid={1}&channel={2}&nonce={3}&timestamp={4}",
                userId, appId, channelId, auth.Nonce, auth.Timestamp);
            System.Console.WriteLine("Sign cost={4}ms, user={0}, userId={1}, token={2}, channelKey={3}",
                user, userId, token, auth.ChannelKey, DateTime.Now.Subtract(starttime).Milliseconds);

            //JObject rturn = new JObject();
            //rturn.Add("username", username);
            //rturn.Add("password", token);

            //JArray rgslbs = new JArray();
            //rgslbs.Add(gslb);

            //JObject rresponse = new JObject();
            //rresponse.Add("appid", appId);
            //rresponse.Add("userid", userId);
            //rresponse.Add("gslb", rgslbs);
            //rresponse.Add("token", token);
            //rresponse.Add("nonce", auth.Nonce);
            //rresponse.Add("timestamp", auth.Timestamp);
            //rresponse.Add("turn", rturn);

            //JObject ro = new JObject();
            //ro.Add("code", 0);
            //ro.Add("data", rresponse);
            return Ok(new
            {
                code=0,
                data = new
                {
                    appid = appId,
                    userid = userId,
                    gslb = new[]{ gslb },
                    token = token,
                    nonce = auth.Nonce,
                    timestamp = auth.Timestamp,
                    turn = new
                    {
                        username = username,
                        password = token,
                    },
                }
            });
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
