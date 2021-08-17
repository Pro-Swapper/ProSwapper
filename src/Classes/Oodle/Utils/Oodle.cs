#nullable enable
using System;
using System.IO;
using System.Text;

namespace Pro_Swapper.Oodle.Utils
{
    // Note: (Tamely) Still not completely cleaned, but WAYYYYY less spaghetti than it was
    public class Oodle
    {
        public static byte[]? SourceArray;
        private static readonly byte[] DestinationArray = new byte[4];
        public static int SourceLength;

        public static void Prepare(string filePath)
        {
            SourceArray = null; // Note: (Tamely) Need to assign null for reuse... just in case
            SourceArray = File.ReadAllBytes(filePath);
            Array.Copy(SourceArray, DestinationArray, 4);
            Array.Copy(SourceArray, 12, DestinationArray, 0, 4);
            var header = Encoding.ASCII.GetString(DestinationArray);
            if (header == "KRKN")
            {
                var targetLength = (int) BitConverter.ToUInt32(SourceArray, 20);
                var targetArray = new byte[targetLength];
                Array.Copy(SourceArray, 24, targetArray, 0, targetLength);
            }

            SourceLength = SourceArray.Length;
        }
        
        public static void Prepare(byte[] array)
        {
            SourceArray = null; // Note: (Tamely) Need to assign null for reuse... just in case
            SourceArray = array;
            Array.Copy(SourceArray, DestinationArray, 4);
            Array.Copy(SourceArray, 12, DestinationArray, 0, 4);
            var header = Encoding.ASCII.GetString(DestinationArray);
            if (header == "KRKN")
            {
                var targetLength = (int) BitConverter.ToUInt32(SourceArray, 20);
                var targetArray = new byte[targetLength];
                Array.Copy(SourceArray, 24, targetArray, 0, targetLength);
            }

            SourceLength = SourceArray.Length;
        }
    }
}
