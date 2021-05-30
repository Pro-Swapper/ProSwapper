using System;
using System.IO;
using Pro_Swapper.Oodle.Utils;
using System.Linq;
namespace Pro_Swapper.Oodle
{
    public class OodleClass
    {

        // KRAKEN
        public static void Compress(string decompressedFilePath, string outputPath)
        {
            Utils.Oodle.Prepare(decompressedFilePath);

            byte[] compressed;
            string string_;
            string_ = outputPath;
            int oodleFormat_ = 8;
            uint uint_;
            try
            {
                uint_ = OodleStream.GetCompressedLength(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, (OodleFormat)oodleFormat_, OodleCompressionLevel.Normal);
            }
            catch (AccessViolationException)
            {
                uint_ = 64U;
            }

            compressed = OodleStream.OodleCompress(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, (OodleFormat)oodleFormat_, OodleCompressionLevel.Normal, uint_);
            Helper.Write(compressed, string_);
        }

        public static byte[] Compress(string decompressedFilePath)
        {
            Utils.Oodle.Prepare(decompressedFilePath);
            int oodleFormat_ = 8;
            uint uint_;
            try
            {
                uint_ = OodleStream.GetCompressedLength(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, (OodleFormat)oodleFormat_, OodleCompressionLevel.Normal);
            }
            catch (AccessViolationException)
            {
                uint_ = 64U;
            }
            return OodleStream.OodleCompress(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, (OodleFormat)oodleFormat_, OodleCompressionLevel.Normal, uint_);
        }

        public static byte[] OverrideBytes(byte[] original, byte[] tooverride)
        {
            for (int i = 0; i < tooverride.Length; i++)
                original[i] = tooverride[i];
            return original;
        }

        public static void Decompress(string compressedFilePath, string outputPath)
        {
            Stream stream = File.OpenRead(compressedFilePath);
            stream.Dispose();
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(compressedFilePath, FileMode.Open, FileAccess.ReadWrite)))
            {
                binaryWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                binaryWriter.Write(OodleStream.OodleKraken);
                binaryWriter.Close();
            }

            Utils.Oodle.Prepare(compressedFilePath);



            Stream stream2 = File.OpenRead(compressedFilePath);
            stream2.Dispose();
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(compressedFilePath, FileMode.Open, FileAccess.ReadWrite)))
            {
                binaryWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                binaryWriter.Write(OodleStream.Zero);
                binaryWriter.Close();
            }

            string fileName = outputPath;
            Helper.Write(OodleStream.OodleDecompress(Utils.Oodle.targetArray, Utils.Oodle.targetLength, Utils.Oodle.placeholderOffset3), fileName);
        }

        // MERMAID

        public static void MermaidCompress(string decompressedFilePath, string outputPath)
        {
            Utils.Oodle.Prepare(decompressedFilePath);

            byte[] compressed;
            string string_;
            string_ = outputPath;
            int oodleFormat_ = 9;
            uint uint_;
            try
            {
                uint_ = OodleStream.GetCompressedLength(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, (OodleFormat)oodleFormat_, OodleCompressionLevel.Normal);
            }
            catch (AccessViolationException)
            {
                uint_ = 64U;
            }

            compressed = OodleStream.OodleCompress(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, (OodleFormat)oodleFormat_, OodleCompressionLevel.Normal, uint_);
            Helper.Write(compressed, string_);
        }

        public static void MermaidDecompress(string compressedFilePath, string outputPath)
        {

            Utils.Oodle.Prepare(compressedFilePath);


            string fileName = outputPath;
            Helper.Write(OodleStream.OodleDecompress(Utils.Oodle.targetArray, Utils.Oodle.targetLength, Utils.Oodle.placeholderOffset3), fileName);
        }


        // NEW Compression 
        /*
        public static void Tamely(string decompressedFilePath, string outputPath)
        {
            Utils.Oodle.Prepare(decompressedFilePath);
            byte[] compressed;
            byte[] error =
            {
                104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81,104,116,116,112,115,58,47,47,119,119,119,46,121,111,117,116,117,98,101,46,99,111,109,47,119,97,116,99,104,63,118,61,100,81,119,52,119,57,87,103,88,99,81
            };

            int oodleFormat_ = 8;
            uint uint_;
            try
            {
                uint_ = OodleStream.GetCompressedLength(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, OodleFormat.Kraken, OodleCompressionLevel.Level5);
            }
            catch (AccessViolationException)
            {
                uint_ = 64U;
            }

            compressed = OodleStream.OodleCompress(Utils.Oodle.sourceArray, Utils.Oodle.sourceLength, OodleFormat.Kraken, OodleCompressionLevel.Level5, uint_);


            Helper.Write(compressed, outputPath);
        }*/
    }
}
