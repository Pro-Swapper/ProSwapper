using System;
using System.IO;
using Pro_Swapper.Oodle.Utils;
namespace Pro_Swapper.Oodle
{
    public class OodleClass
    {
        //Pro Swapper doesnt need this so we'll just comment it out.
        /*
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


            WriteFile(compressed, outputPath); // Writing the data
        }

        public static void WriteFile(byte[] writableData, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.IsReadOnly && File.Exists(filePath))
                fileInfo.IsReadOnly = false;
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(writableData);
            binaryWriter.Close();
            fileStream.Close();
        }*/

        public static byte[] Compress(byte[] array) // Leaving incase you want a save file one
        {
            Utils.Oodle.Prepare(array); // Gets the source prepared
            uint @uint; // Needs to be outside so it always has a value
            try
            {
                @uint = OodleStream.GetCompressedLength(Utils.Oodle.SourceArray, Utils.Oodle.SourceArray.Length, OodleFormat.Kraken, OodleCompressionLevel.Level5);
            }
            catch (AccessViolationException)
            {
                @uint = 64U;
            }

            return OodleStream.OodleCompress(Utils.Oodle.SourceArray, Utils.Oodle.SourceArray.Length, OodleFormat.Kraken, OodleCompressionLevel.Level5, @uint);
        }
    }
}
