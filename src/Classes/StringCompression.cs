using System.IO;

namespace Pro_Swapper
{
    public static class ByteCompression
    {
        public static byte[] Decompress(byte[] input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Ionic.Zlib.ZlibStream zls = new Ionic.Zlib.ZlibStream(ms, Ionic.Zlib.CompressionMode.Decompress, Ionic.Zlib.CompressionLevel.BestCompression))
                    zls.Write(input, 0, input.Length);

                return ms.ToArray();
            }
        }
        public static byte[] Compress(byte[] input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Ionic.Zlib.ZlibStream zls = new Ionic.Zlib.ZlibStream(ms, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
                    zls.Write(input, 0, input.Length);

                return ms.ToArray();
            }
        }
    }
}
