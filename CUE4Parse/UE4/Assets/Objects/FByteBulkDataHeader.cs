using System;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Versions;
using CUE4Parse.Utils;
using Newtonsoft.Json;
using static CUE4Parse.UE4.Assets.Objects.EBulkDataFlags;

namespace CUE4Parse.UE4.Assets.Objects
{
    [JsonConverter(typeof(FByteBulkDataHeaderConverter))]
    public readonly struct FByteBulkDataHeader
    {
        public readonly EBulkDataFlags BulkDataFlags;
        public readonly int ElementCount;
        public readonly uint SizeOnDisk;
        public readonly long OffsetInFile;

        public FByteBulkDataHeader(FAssetArchive Ar)
        {
            BulkDataFlags = Ar.Read<EBulkDataFlags>();
            ElementCount = BulkDataFlags.HasFlag(BULKDATA_Size64Bit) ? (int) Ar.Read<long>() : Ar.Read<int>();
            SizeOnDisk = BulkDataFlags.HasFlag(BULKDATA_Size64Bit) ? (uint) Ar.Read<long>() : Ar.Read<uint>();
            OffsetInFile = Ar.Ver >= EUnrealEngineObjectUE4Version.BULKDATA_AT_LARGE_OFFSETS ? Ar.Read<long>() : Ar.Read<int>();
            if (!BulkDataFlags.HasFlag(BULKDATA_NoOffsetFixUp)) // UE4.26 flag
            {
                OffsetInFile += Ar.Owner.Summary.BulkDataStartOffset;
            }

            if (BulkDataFlags.HasFlag(BULKDATA_BadDataVersion))
            {
                Ar.Position += sizeof(ushort);
                BulkDataFlags &= ~BULKDATA_BadDataVersion;
            }
        }
    }

    public class FByteBulkDataHeaderConverter : JsonConverter<FByteBulkDataHeader>
    {
        public override void WriteJson(JsonWriter writer, FByteBulkDataHeader value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("BulkDataFlags");
            writer.WriteValue(value.BulkDataFlags.ToStringBitfield());

            writer.WritePropertyName("ElementCount");
            writer.WriteValue(value.ElementCount);

            writer.WritePropertyName("SizeOnDisk");
            writer.WriteValue(value.SizeOnDisk);

            writer.WritePropertyName("OffsetInFile");
            writer.WriteValue($"0x{value.OffsetInFile:X}");

            writer.WriteEndObject();
        }

        public override FByteBulkDataHeader ReadJson(JsonReader reader, Type objectType, FByteBulkDataHeader existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}