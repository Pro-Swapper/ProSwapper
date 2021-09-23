using System.IO;
using Ionic.Zlib;
namespace Pro_Swapper
{
    public static class ByteCompression
    {
        public static byte[] Decompress(byte[] input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZlibStream zls = new ZlibStream(ms, CompressionMode.Decompress, CompressionLevel.BestCompression))
                    zls.Write(input, 0, input.Length);

                return ms.ToArray();
            }
        }
        public static byte[] Compress(byte[] input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZlibStream zls = new ZlibStream(ms, CompressionMode.Compress, CompressionLevel.BestCompression))
                    zls.Write(input, 0, input.Length);

                return ms.ToArray();
            }
        }
    }
}
