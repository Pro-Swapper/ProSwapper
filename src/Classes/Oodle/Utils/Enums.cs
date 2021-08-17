namespace Pro_Swapper.Oodle.Utils
{
    public enum BaseEnum : uint
    {
        Const0 = 6U
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

    public enum CompressionType : uint // Note: (Tamely) Used for decompression so not needed
    {
        Unknown,
        Oodle,
        Zlib
    }
}
