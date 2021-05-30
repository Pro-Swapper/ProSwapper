using System.IO;
namespace Pro_Swapper.Oodle.Utils
{
    public class Helper
    {
        public static void Write(byte[] writableData, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.IsReadOnly && File.Exists(filePath))
            {
                fileInfo.IsReadOnly = false;
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(writableData);
            binaryWriter.Close();
            fileStream.Close();
        }
    }
}
