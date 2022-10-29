using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
namespace Pro_Swapper
{

    //From https://github.com/Tamely/SaturnSwapper/blob/cbe79e66fcbc900f8bb6c45387f01a39fa127b01/Saturn.Backend/Core/Utils/Compression/Oodle.cs

    public static class Oodle
    {
        [DllImport(Program.Oodledll)]
        private static extern int OodleLZ_Compress(OodleFormat Format, byte[] Buffer, long BufferSize, byte[] OutputBuffer, OodleCompressionLevel Level, uint a, uint b, uint c);
        private static uint CompressStream(byte[] Buffer, uint BufferSize, ref byte[] OutputBuffer, uint OutputBufferSize, OodleFormat Format, OodleCompressionLevel Level)
        {
            if (Buffer.Length > 0 && BufferSize > 0 && OutputBuffer.Length > 0 && OutputBufferSize > 0)
                return (uint)OodleLZ_Compress(Format, Buffer, BufferSize, OutputBuffer, Level, 0, 0, 0);

            return 0;
        }

        public static uint GetCompressedBounds(uint BufferSize)
        {
            return BufferSize + 274 * ((BufferSize + 0x3FFFF) / 0x40000);
        }

        public static byte[] Compress(byte[] Buffer, OodleFormat oodleFormat = OodleFormat.OodleLZ_Compressor_Kraken, OodleCompressionLevel oodleCompressionLevel = OodleCompressionLevel.OodleLZ_CompressionLevel_Optimal5)
        {
            var MaxLength = GetCompressedBounds((uint)Buffer.Length);
            var OutputBuffer = new byte[MaxLength];

            var CompressedSize = CompressStream(Buffer, (uint)Buffer.Length, ref OutputBuffer, MaxLength,
                oodleFormat, oodleCompressionLevel);

            if (CompressedSize < 0)
                throw new InvalidDataException("Unable to compress buffer.");

            var tempBuffer = new byte[CompressedSize];
            Array.Copy(OutputBuffer, tempBuffer, CompressedSize);

            return tempBuffer;

        }



        public static KeyValuePair<OodleFormat, OodleCompressionLevel> FindBestCompressionSettings(byte[] buffer)
        {
            var bestSize = int.MaxValue;
            var bestFormat = OodleFormat.OodleLZ_Compressor_Kraken;
            var bestLevel = OodleCompressionLevel.OodleLZ_CompressionLevel_Fast;

            foreach (OodleFormat format in Enum.GetValues(typeof(OodleFormat)))
            {
                foreach (OodleCompressionLevel level in Enum.GetValues(typeof(OodleCompressionLevel)))
                {
                    var compressed = Compress(buffer, format, level);
                    if (compressed.Length < bestSize)
                    {
                        bestSize = compressed.Length;
                        bestFormat = format;
                        bestLevel = level;
                    }
                }
            }

            return new KeyValuePair<OodleFormat, OodleCompressionLevel>(bestFormat, bestLevel);
        }
    }
}

//https://github.com/EpicGames/UnrealEngine/blob/46544fa5e0aa9e6740c19b44b0628b72e7bbd5ce/Engine/Source/Programs/Horde/HordeStorage/Jupiter.Common/Utils/OodleCompressor.cs#L10
public enum OodleFormat
{
    //OodleLZ_Compressor_Invalid = -1,
    //OodleLZ_Compressor_None = 3, // None = memcpy, pass through uncompressed bytes

    // NEW COMPRESSORS :
    OodleLZ_Compressor_Kraken = 8, // Fast decompression and high compression ratios, amazing!
                                   //OodleLZ_Compressor_Leviathan = 13, // Leviathan = Kraken's big brother with higher compression, slightly slower decompression.
                                   //OodleLZ_Compressor_Mermaid = 9, // Mermaid is between Kraken & Selkie - crazy fast, still decent compression.
                                   //OodleLZ_Compressor_Selkie = 11, // Selkie is a super-fast relative of Mermaid.  For maximum decode speed.
                                   //OodleLZ_Compressor_Hydra = 12, // Hydra, the many-headed beast = Leviathan, Kraken, Mermaid, or Selkie (see $OodleLZ_About_Hydra)
                                   //OodleLZ_Compressor_LZB16 = 4, //still supported




    /* Deprecated compressors
    OodleLZ_Compressor_BitKnit = 10, // no longer supported as of Oodle 2.9.0
    OodleLZ_Compressor_LZB16 = 4, // DEPRECATED but still supported
    OodleLZ_Compressor_LZNA = 7,  // no longer supported as of Oodle 2.9.0
    OodleLZ_Compressor_LZH = 0,   // no longer supported as of Oodle 2.9.0
    OodleLZ_Compressor_LZHLW = 1, // no longer supported as of Oodle 2.9.0
    OodleLZ_Compressor_LZNIB = 2, // no longer supported as of Oodle 2.9.0
    OodleLZ_Compressor_LZBLW = 5, // no longer supported as of Oodle 2.9.0
    OodleLZ_Compressor_LZA = 6,   // no longer supported as of Oodle 2.9.0
     */

    //    OodleLZ_Compressor_Count = 14,
    //OodleLZ_Compressor_Force32 = 0x40000000
};

//https://github.com/EpicGames/UnrealEngine/blob/46544fa5e0aa9e6740c19b44b0628b72e7bbd5ce/Engine/Source/Programs/Horde/HordeStorage/Jupiter.Common/Utils/OodleCompressor.cs#L37
public enum OodleCompressionLevel
{
    OodleLZ_CompressionLevel_None = 0,        // don't compress, just copy raw bytes
    OodleLZ_CompressionLevel_SuperFast = 1,   // super fast mode, lower compression ratio
    OodleLZ_CompressionLevel_VeryFast = 2,    // fastest LZ mode with still decent compression ratio
    OodleLZ_CompressionLevel_Fast = 3,        // fast - good for daily use
    OodleLZ_CompressionLevel_Normal = 4,      // standard medium speed LZ mode

    OodleLZ_CompressionLevel_Optimal1 = 5,    // optimal parse level 1 (faster optimal encoder)
    OodleLZ_CompressionLevel_Optimal2 = 6,    // optimal parse level 2 (recommended baseline optimal encoder)
    OodleLZ_CompressionLevel_Optimal3 = 7,    // optimal parse level 3 (slower optimal encoder)
    OodleLZ_CompressionLevel_Optimal4 = 8,    // optimal parse level 4 (very slow optimal encoder)
    OodleLZ_CompressionLevel_Optimal5 = 9,    // optimal parse level 5 (don't care about encode speed, maximum compression)

    OodleLZ_CompressionLevel_HyperFast1 = -1, // faster than SuperFast, less compression
    OodleLZ_CompressionLevel_HyperFast2 = -2, // faster than HyperFast1, less compression
    OodleLZ_CompressionLevel_HyperFast3 = -3, // faster than HyperFast2, less compression
    OodleLZ_CompressionLevel_HyperFast4 = -4, // fastest, less compression

    // aliases :
    //OodleLZ_CompressionLevel_HyperFast = OodleLZ_CompressionLevel_HyperFast1, // alias hyperfast base level
    //OodleLZ_CompressionLevel_Optimal = OodleLZ_CompressionLevel_Optimal2,   // alias optimal standard level
    //OodleLZ_CompressionLevel_Max = OodleLZ_CompressionLevel_Optimal5,   // maximum compression level
    //OodleLZ_CompressionLevel_Min = OodleLZ_CompressionLevel_HyperFast4, // fastest compression level

    //OodleLZ_CompressionLevel_Force32 = 0x40000000,
    //OodleLZ_CompressionLevel_Invalid = OodleLZ_CompressionLevel_Force32
}