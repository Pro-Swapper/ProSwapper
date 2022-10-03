using System;
using System.IO;
using System.Runtime.InteropServices;
namespace Pro_Swapper
{

    //From https://github.com/Tamely/SaturnSwapper/blob/cbe79e66fcbc900f8bb6c45387f01a39fa127b01/Saturn.Backend/Core/Utils/Compression/Oodle.cs

    public static class Oodle
    {
        [DllImport(Program.Oodledll)]
        private static extern int OodleLZ_Compress(OodleFormat Format, byte[] Buffer, long BufferSize, byte[] OutputBuffer, OodleCompressionLevel Level, uint a, uint b, uint c);
        private static uint CompressStream(byte[] Buffer, uint BufferSize, ref byte[] OutputBuffer, uint OutputBufferSize,OodleFormat Format, OodleCompressionLevel Level)
        {
            if (Buffer.Length > 0 && BufferSize > 0 && OutputBuffer.Length > 0 && OutputBufferSize > 0)
                return (uint)OodleLZ_Compress(Format, Buffer, BufferSize, OutputBuffer, Level, 0, 0, 0);

            return 0;
        }

        public static uint GetCompressedBounds(uint BufferSize)
        {
            return BufferSize + 274 * ((BufferSize + 0x3FFFF) / 0x40000);
        }
        
        public static byte[] Compress(byte[] Buffer, OodleFormat oodleFormat = OodleFormat.Kraken )
        {
            var MaxLength = GetCompressedBounds((uint)Buffer.Length);
            var OutputBuffer = new byte[MaxLength];

            var CompressedSize = CompressStream(Buffer, (uint)Buffer.Length, ref OutputBuffer, MaxLength,
                oodleFormat, OodleCompressionLevel.Optimal5);
            
            if (CompressedSize < 0)
                throw new InvalidDataException("Unable to compress buffer.");

            var tempBuffer = new byte[CompressedSize];
            Array.Copy(OutputBuffer, tempBuffer, CompressedSize);

            return tempBuffer;

        }
    }

    public enum OodleFormat : uint
    {
        LZH = 0,
        LZHLW = 1,
        LZNIB = 2,
        None = 3,
        LZB16 = 4,
        LZBLW = 5,
        LZA = 6,
        LZNA = 7,
        Kraken = 8,
        Mermaid = 9,
        BitKnit = 10,
        Selkie = 11,
        Hydra = 12,
        Leviathan = 13
    }

    public enum OodleCompressionLevel : uint
    {
        None = 0,
        SuperFast = 1,
        VeryFast = 2,
        Fast = 3,
        Normal = 4,
        Optimal1 = 5,
        Optimal2 = 6,
        Optimal3 = 7,
        Optimal4 = 8,
        Optimal5 = 9
    }
}
