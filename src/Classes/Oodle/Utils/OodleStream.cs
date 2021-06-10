using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Pro_Swapper.Oodle.Utils
{
	public class OodleStream
	{
		//oo2core_8_win64.dll


		private const string oodledll = "oo2core_5_win64.dll";
		[DllImport(oodledll)]
		public static extern int OodleLZ_Compress(OodleFormat format, byte[] decompressedBuffer, long decompressedSize, byte[] compressedBuffer, OodleCompressionLevel compressionLevel, uint uint_0, uint uint_1, uint uint_2, BaseEnum genum);

		[DllImport(oodledll)]
		private static extern int OodleLZ_Decompress(byte[] compressedBuffer, long compressedSize, byte[] decompressedBuffer, long decompressedSize, uint uint_0, uint uint_1, ulong ulong_0, uint uint_2, uint uint_3, uint uint_4, uint uint_5, uint uint_6, uint uint_7, uint uint_8);

		public static byte[] OodleCompress(byte[] decompressedBuffer, int decompressedSize, OodleFormat format, OodleCompressionLevel compressionLevel, uint uintHomie)
		{
			byte[] array = new byte[GetDecompressedLength((uint)decompressedSize)];
			var num = OodleLZ_Compress(format, decompressedBuffer, decompressedSize, array, compressionLevel, 0U, 0U, 0U, 0U);
			var num2 = num - (int)uintHomie;
			var num3 = 0;
			var compressedByte = new byte[uintHomie + (uint)num2 + (uint)num3];
			Buffer.BlockCopy(array, 0, compressedByte, num3, num);
			return compressedByte;
		}

		public static byte[] OodleDecompress(byte[] compressedBuffer, int compressedLength, int decompressedSize)
		{
			var array = new byte[decompressedSize];
			var num = OodleLZ_Decompress(compressedBuffer, compressedLength, array, decompressedSize, 0U, 0U, 0UL, 0U, 0U, 0U, 0U, 0U, 0U, 3U);
			if (num == decompressedSize)
			{
				return array;
			}
			if (num >= decompressedSize)
			{
				throw new Exception("There was an error while decompressing");
			}
			return array.Take(num).ToArray();
		}

		internal static uint GetDecompressedLength(uint length)
		{
			return length + 274U * ((length + 262143U) / 262144U);
		}

		public static uint GetCompressedLength(byte[] decompressedBuffer, int decompressedSize, OodleFormat format, OodleCompressionLevel compression)
		{
            //return (uint)SizeIt(decompressedBuffer, decompressedSize, format, compression);
            return (uint)Tamely.SizeIt(decompressedBuffer, decompressedSize, format, compression);
		}

		public static byte[] OodleKraken =
		{
			
			// 4F 4F 44 4C 00 00 00 00 30 00 06 2E 4B 52 4B 4E
			// OODL        KRKN
			79,
			79,
			68,
			76,
			0,
			0,
			0,
			0,
			48,
			0,
			6,
			46,
			75,
			82,
			75,
			78
		};

		public static byte[] Zero =
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};
	}
}
