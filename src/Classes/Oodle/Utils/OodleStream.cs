using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Pro_Swapper.Oodle.Utils
{
	public class OodleStream
	{
		[DllImport("oo2core_5_win64.dll")]
		private static extern int OodleLZ_Compress(OodleFormat format, byte[] decompressedBuffer, long decompressedSize,
			byte[] compressedBuffer, OodleCompressionLevel compressionLevel, uint uint0, uint uint1, uint uint2,
			BaseEnum genum);

		public static byte[] OodleCompress(byte[]? decompressedBuffer, int decompressedSize, OodleFormat format,
			OodleCompressionLevel compressionLevel, uint uintHomie)
		{
			var array = new byte[GetCompressedBuffer((uint) decompressedSize)];
			var num = OodleLZ_Compress(format, decompressedBuffer, decompressedSize, array, compressionLevel, 0U, 0U,
				0U, 0U);
			var num2 = num - (int) uintHomie;
			var num3 = 0;
			var compressedByte = new byte[uintHomie + (uint) num2 + (uint) num3];
			Buffer.BlockCopy(array, 0, compressedByte, num3, num);
			return compressedByte;
		}


		private static uint GetCompressedBuffer(uint length)
		{
			return length + 274U * ((length + 262143U) / 262144U);
		}

		public static uint GetCompressedLength(byte[]? decompressedBuffer, int decompressedSize, OodleFormat format,
			OodleCompressionLevel compression)
		{
			var temp = new byte[(int) GetCompressedBuffer((uint) decompressedSize)];
			return (uint) OodleLZ_Compress(format, decompressedBuffer, decompressedSize, temp, compression, 0U, 0U, 0U,
				0);
		}
	}
}
