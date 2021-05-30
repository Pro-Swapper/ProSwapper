namespace Pro_Swapper.Oodle.Utils
{
    public sealed class Tamely
    {
        public static int SizeIt(byte[] buffer, int size, OodleFormat format, OodleCompressionLevel compressionLevel)
        {
            var temp = new byte[(int)OodleStream.GetDecompressedLength((uint)size)];
            return OodleStream.OodleLZ_Compress(format, buffer, size, temp, compressionLevel, 0U, 0U, 0U, 0);
        }

    }
}