using System;
using System.Runtime.InteropServices;
namespace Pro_Swapper.Oodle
{

    //Credit to Tamely https://github.com/Tamely/Oodle-Compressor
    public class OodleClass
    {
        public static byte[] Compress(byte[] buffer)
        {
            uint @uint; // Needs to be outside so it always has a value
            try
            {
                @uint = (uint)OodleStream.OodleLZ_Compress(OodleFormat.Kraken, buffer, // Get decompressed buffer
                    buffer.Length, // Get decompressed length
                    new byte[(int)(uint)buffer.Length + 274U *
                        (((uint)buffer.Length + 262143U) / 262144U)], // Get compressed size
                    OodleCompressionLevel.Level5, 0U, 0U, 0U, 0);
            }
            catch (AccessViolationException)
            {
                @uint = 64U; // Just in case there is protected memory
            }

            return OodleStream.OodleCompress(buffer, buffer.Length, OodleFormat.Kraken, OodleCompressionLevel.Level5, @uint); // Writing the data
        }
    }

    public class OodleStream
    {
        [DllImport(Program.Oodledll)]
        public static extern int OodleLZ_Compress(OodleFormat format, byte[]? decompressedBuffer, long decompressedSize, byte[] compressedBuffer, OodleCompressionLevel compressionLevel, uint a, uint b, uint c, ThreadModule threadModule); // Oodle dll method

        public static byte[] OodleCompress(byte[]? decompressedBuffer, int decompressedSize, OodleFormat format, OodleCompressionLevel compressionLevel, uint a)
        {
            var array = new byte[(uint)decompressedSize + 274U * (((uint)decompressedSize + 262143U) / 262144U)]; // Initializes array with compressed array size
            var compressedBytes = new byte[a + (uint)OodleLZ_Compress(format, decompressedBuffer, // Initializes the array we will be returning
                decompressedSize, array, compressionLevel, 0U, 0U,
                0U, 0U) - (int)a];
            Buffer.BlockCopy(array, 0, compressedBytes, 0, OodleLZ_Compress(format, decompressedBuffer, decompressedSize,
                array, compressionLevel, 0U, 0U,
                0U, 0U)); // Combines the two arrays
            return compressedBytes;
        }
    }

    public enum ThreadModule : uint
    {
    }

    public enum OodleFormat : uint
    {
        Lzh,
        Lzhlw,
        Lznib,
        Lzb,
        Lzb16,
        Lzblw,
        Lza,
        Lzna,
        Kraken,
        Mermaid,
        BitKnit,
        Selkie,
        Akkorokamui,
        None
    }

    public enum OodleCompressionLevel : ulong
    {
        None,
        Fastest,
        Faster,
        Fast,
        Normal,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    public enum CompressionType : uint // Used for decompression so not needed here, unless someone wants to add it
    {
        Unknown,
        Oodle,
        Zlib
    }
}
