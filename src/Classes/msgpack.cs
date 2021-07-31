using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
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
        public static T MsgPacklz4<T>(string url)
        {
            var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray).WithSecurity(MessagePackSecurity.UntrustedData);
            var allskinslz4 = MessagePackSerializer.Deserialize<dynamic>(global.web.DownloadData(url), lz4Options);
            string json = MessagePackSerializer.SerializeToJson(allskinslz4, lz4Options);

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Input a url which responds with msgpack compressed in lz4, responds with json object which is 'T'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static dynamic MsgPacklz4(string url)
        {
            var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray).WithSecurity(MessagePackSecurity.UntrustedData);
            var allskinslz4 = MessagePackSerializer.Deserialize<dynamic>(global.web.DownloadData(url), lz4Options);
            string json = MessagePackSerializer.SerializeToJson(allskinslz4, lz4Options);

            return (dynamic)JObject.Parse(json);
        }
    }
}
