﻿using MessagePack;
using Newtonsoft.Json.Linq;

namespace Pro_Swapper
{
    public static class msgpack
    {
        /// <summary>
        /// Input a url which responds with msgpack compressed in lz4, responds with json object which is 'T'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        //public static T MsgPacklz4<T>(string url)
        //{
        //    var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Proxy = null;
        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    {
        //        var AllCosmeticsLz4 = MessagePackSerializer.Deserialize<dynamic>(response.GetResponseStream(), lz4Options);
        //        string json = MessagePackSerializer.SerializeToJson(AllCosmeticsLz4, lz4Options);
        //        return JsonConvert.DeserializeObject<T>(json);
        //    }
        //}

        public static dynamic MsgPacklz4(string url)
        {
            var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray).WithSecurity(MessagePackSecurity.UntrustedData);
            var allskinslz4 = MessagePackSerializer.Deserialize<dynamic>(Program.httpClient.GetByteArrayAsync(url).GetAwaiter().GetResult(), lz4Options);
            string json = MessagePackSerializer.SerializeToJson(allskinslz4, lz4Options);

            return (dynamic)JObject.Parse(json);
        }
    }
}
