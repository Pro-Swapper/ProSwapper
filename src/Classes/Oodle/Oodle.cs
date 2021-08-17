using System;
using Pro_Swapper.Oodle.Utils;
namespace Pro_Swapper.Oodle
{
    public class OodleClass
    {

        public static void Compress(string decompressedFilePath, string outputPath) // Leaving incase you want a save file one
        {
            Utils.Oodle.Prepare(decompressedFilePath); // Gets the source prepared
            uint @uint; // Needs to be outside so it always has a value
            try
            {
                @uint = OodleStream.GetCompressedLength(Utils.Oodle.SourceArray, Utils.Oodle.SourceLength,
                    OodleFormat.Kraken, OodleCompressionLevel.Level5);
            }
            catch (AccessViolationException)
            {
                @uint = 64U;
            }

            var compressed = OodleStream.OodleCompress(Utils.Oodle.SourceArray, Utils.Oodle.SourceLength,
                OodleFormat.Kraken, OodleCompressionLevel.Level5, @uint);


            Helper.Write(compressed, outputPath); // Writing the data
        }
        
        public static byte[] Compress(byte[] array) // Leaving incase you want a save file one
        {
            Utils.Oodle.Prepare(array); // Gets the source prepared
            uint @uint; // Needs to be outside so it always has a value
            try
            {
                @uint = OodleStream.GetCompressedLength(Utils.Oodle.SourceArray, Utils.Oodle.SourceLength,
                    OodleFormat.Kraken, OodleCompressionLevel.Level5);
            }
            catch (AccessViolationException)
            {
                @uint = 64U;
            }

            return OodleStream.OodleCompress(Utils.Oodle.SourceArray, Utils.Oodle.SourceLength,
                OodleFormat.Kraken, OodleCompressionLevel.Level5, @uint);
        }
    }
}
