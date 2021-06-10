using System;
using System.IO;
using System.Text;

namespace Pro_Swapper.Oodle.Utils
{
    public class Oodle
    {
        public static byte[] sourceArray;

        public static byte[] targetArray;

        private static byte[] destinationArray = new byte[4];

        public static string destEncodedString;

        public static string string_1;

        public static int placeholderOffset2;

        public static int placeholderOffset1;

        public static int targetLength;

        public static int placeholderOffset3;

        public static int sourceLength;

        public static byte[] Prepare(string filePath)
        {
            placeholderOffset2 = 0;
            placeholderOffset1 = 13;
            sourceArray = null;
            Oodle @class = new Oodle();
            sourceArray = @class.Writer(filePath);
            Array.Copy(sourceArray, destinationArray, 4);
            destEncodedString = Encoding.ASCII.GetString(destinationArray);
            if (destEncodedString == "OODL")
            {
                placeholderOffset2 = 1;
            }
            Array.Copy(sourceArray, 12, destinationArray, 0, 4);
            string_1 = Encoding.ASCII.GetString(destinationArray);
            if (string_1 == "KRKN")
            {
                placeholderOffset1 = 8;
                placeholderOffset3 = (int)BitConverter.ToUInt32(sourceArray, 16);
                targetLength = (int)BitConverter.ToUInt32(sourceArray, 20);
                targetArray = new byte[targetLength];
                Array.Copy(sourceArray, 24, targetArray, 0, targetLength);
            }
            sourceLength = sourceArray.Length;
            return sourceArray;
        }

        public static byte[] Prepare(byte[] Bytes)
        {
            placeholderOffset2 = 0;
            placeholderOffset1 = 13;
            sourceArray = null;
            sourceArray = Bytes;
            Array.Copy(sourceArray, destinationArray, 4);
            destEncodedString = Encoding.ASCII.GetString(destinationArray);
            if (destEncodedString == "OODL")
            {
                placeholderOffset2 = 1;
            }
            Array.Copy(sourceArray, 12, destinationArray, 0, 4);
            string_1 = Encoding.ASCII.GetString(destinationArray);
            if (string_1 == "KRKN")
            {
                placeholderOffset1 = 8;
                placeholderOffset3 = (int)BitConverter.ToUInt32(sourceArray, 16);
                targetLength = (int)BitConverter.ToUInt32(sourceArray, 20);
                targetArray = new byte[targetLength];
                Array.Copy(sourceArray, 24, targetArray, 0, targetLength);
            }
            sourceLength = sourceArray.Length;
            return sourceArray;
        }


        private byte[] Writer(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            long length = new FileInfo(fileName).Length;
            byte[] result = binaryReader.ReadBytes((int)length);
            binaryReader.Close();
            fileStream.Close();
            return result;
        }
    }
}
