using System.Runtime.InteropServices;

using CUE4Parse.UE4.Objects.Core.Serialization;
using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse.UE4.Readers;
using CUE4Parse.UE4.Versions;

namespace CUE4Parse.UE4.IO.Objects
{
    public enum EZenPackageVersion : uint
    {
        Initial,

        LatestPlusOne,
        Latest = LatestPlusOne - 1
    }

    public struct FZenPackageVersioningInfo
    {
        public EZenPackageVersion ZenVersion;
        public FPackageFileVersion PackageVersion;
        public int LicenseeVersion;
        public FCustomVersion[] CustomVersions;

        public FZenPackageVersioningInfo(FArchive Ar)
        {
            ZenVersion = Ar.Read<EZenPackageVersion>();
            PackageVersion = Ar.Read<FPackageFileVersion>();
            LicenseeVersion = Ar.Read<int>();
            CustomVersions = Ar.ReadArray<FCustomVersion>();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct FZenPackageSummary
    {
        public readonly uint bHasVersioningInfo;
        public readonly uint HeaderSize;
        public readonly FMappedName Name;
        public readonly EPackageFlags PackageFlags;
        public readonly uint CookedHeaderSize;
        public readonly int ImportedPublicExportHashesOffset;
        public readonly int ImportMapOffset;
        public readonly int ExportMapOffset;
        public readonly int ExportBundleEntriesOffset;
        public readonly int GraphDataOffset;
    }
}
