using System;
using CUE4Parse.UE4.Assets.Exports.StaticMesh;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Objects.Engine;
using CUE4Parse.UE4.Objects.Meshes;
using CUE4Parse.UE4.Readers;
using CUE4Parse.UE4.Versions;
using Newtonsoft.Json;

namespace CUE4Parse.UE4.Assets.Exports.SkeletalMesh
{
    public enum EClassDataStripFlag : byte
    {
        CDSF_AdjacencyData = 1,
        CDSF_MinLodData = 2,
    };

    [JsonConverter(typeof(FStaticLODModelConverter))]
    public class FStaticLODModel
    {
        public FSkelMeshSection[] Sections;
        public FMultisizeIndexContainer? Indices;
        public short[] ActiveBoneIndices;
        public FSkelMeshChunk[] Chunks;
        public int Size;
        public int NumVertices;
        public short[] RequiredBones;
        public FIntBulkData RawPointIndices;
        public int[] MeshToImportVertexMap;
        public int MaxImportVertex;
        public int NumTexCoords;
        public FSkeletalMeshVertexBuffer VertexBufferGPUSkin;
        public FSkeletalMeshVertexColorBuffer ColorVertexBuffer;
        public FMultisizeIndexContainer AdjacencyIndexBuffer;
        public FSkeletalMeshVertexClothBuffer ClothVertexBuffer;
        public bool SkipLod => Indices == null || Indices.Indices16.Length < 1 && Indices.Indices32.Length < 1;

        public FStaticLODModel()
        {
            Chunks = Array.Empty<FSkelMeshChunk>();
            MeshToImportVertexMap = Array.Empty<int>();
            ColorVertexBuffer = new FSkeletalMeshVertexColorBuffer();
        }

        public FStaticLODModel(FAssetArchive Ar, bool bHasVertexColors) : this()
        {
            if (Ar.Game == EGame.GAME_SeaOfThieves) Ar.Position += 4;
            var stripDataFlags = Ar.Read<FStripDataFlags>();
            var skelMeshVer = FSkeletalMeshCustomVersion.Get(Ar);
            if (Ar.Game == EGame.GAME_SeaOfThieves) Ar.Position += 4;

            Sections = Ar.ReadArray(() => new FSkelMeshSection(Ar));

            if (skelMeshVer < FSkeletalMeshCustomVersion.Type.SplitModelAndRenderData)
            {
                Indices = new FMultisizeIndexContainer(Ar);
            }
            else
            {
                // UE4.19+ uses 32-bit index buffer (for editor data)
                Indices = new FMultisizeIndexContainer {Indices32 = Ar.ReadBulkArray<uint>()};
            }

            ActiveBoneIndices = Ar.ReadArray<short>();

            if (skelMeshVer < FSkeletalMeshCustomVersion.Type.CombineSectionWithChunk)
            {
                Chunks = Ar.ReadArray(() => new FSkelMeshChunk(Ar));
            }

            Size = Ar.Read<int>();
            if (!stripDataFlags.IsDataStrippedForServer())
                NumVertices = Ar.Read<int>();

            RequiredBones = Ar.ReadArray<short>();
            if (!stripDataFlags.IsEditorDataStripped())
                RawPointIndices = new FIntBulkData(Ar);

            if (Ar.Game != EGame.GAME_StateOfDecay2 && Ar.Ver >= EUnrealEngineObjectUE4Version.ADD_SKELMESH_MESHTOIMPORTVERTEXMAP)
            {
                MeshToImportVertexMap = Ar.ReadArray<int>();
                MaxImportVertex = Ar.Read<int>();
            }

            if (!stripDataFlags.IsDataStrippedForServer())
            {
                NumTexCoords = Ar.Read<int>();
                if (skelMeshVer < FSkeletalMeshCustomVersion.Type.SplitModelAndRenderData)
                {
                    VertexBufferGPUSkin = new FSkeletalMeshVertexBuffer(Ar);
                    if (skelMeshVer >= FSkeletalMeshCustomVersion.Type.UseSeparateSkinWeightBuffer)
                    {
                        var skinWeights = new FSkinWeightVertexBuffer(Ar, VertexBufferGPUSkin.bExtraBoneInfluences);
                        if (skinWeights.Weights.Length > 0)
                        {
                            // Copy data to VertexBufferGPUSkin
                            if (VertexBufferGPUSkin.bUseFullPrecisionUVs)
                            {
                                for (var i = 0; i < NumVertices; i++)
                                {
                                    VertexBufferGPUSkin.VertsFloat[i].Infs = skinWeights.Weights[i];
                                }
                            }
                            else
                            {
                                for (var i = 0; i < NumVertices; i++)
                                {
                                    VertexBufferGPUSkin.VertsHalf[i].Infs = skinWeights.Weights[i];
                                }
                            }
                        }
                    }

                    if (bHasVertexColors)
                    {
                        if (skelMeshVer < FSkeletalMeshCustomVersion.Type.UseSharedColorBufferFormat)
                        {
                            ColorVertexBuffer = new FSkeletalMeshVertexColorBuffer(Ar);
                        }
                        else
                        {
                            var newColorVertexBuffer = new FColorVertexBuffer(Ar);
                            ColorVertexBuffer = new FSkeletalMeshVertexColorBuffer(newColorVertexBuffer.Data);
                        }
                    }

                    if (Ar.Ver < EUnrealEngineObjectUE4Version.REMOVE_EXTRA_SKELMESH_VERTEX_INFLUENCES)
                        throw new ParserException("Unsupported: extra SkelMesh vertex influences (old mesh format)");

                    // https://github.com/gildor2/UEViewer/blob/master/Unreal/UnrealMesh/UnMesh4.cpp#L1415
                    if (Ar.Game == EGame.GAME_StateOfDecay2)
                    {
                        Ar.Position += 8;
                        return;
                    }

                    if (Ar.Game == EGame.GAME_SeaOfThieves)
                    {
                        var arraySize = Ar.Read<int>();
                        Ar.Position += arraySize * 44;

                        for (var i = 0; i < 4; i++)
                        {
                            Ar.ReadArray<int>(); // 4 arrays worth
                        }

                        Ar.Position += 13;
                    }

                    if (!stripDataFlags.IsClassDataStripped((byte) EClassDataStripFlag.CDSF_AdjacencyData))
                        AdjacencyIndexBuffer = new FMultisizeIndexContainer(Ar);

                    if (Ar.Ver >= EUnrealEngineObjectUE4Version.APEX_CLOTH && HasClothData())
                        ClothVertexBuffer = new FSkeletalMeshVertexClothBuffer(Ar);
                }
            }

            if (Ar.Game == EGame.GAME_SeaOfThieves)
            {
                var _ = new FMultisizeIndexContainer(Ar);
            }
        }

        // UE ref https://github.com/EpicGames/UnrealEngine/blob/26450a5a59ef65d212cf9ce525615c8bd673f42a/Engine/Source/Runtime/Engine/Private/SkeletalMeshLODRenderData.cpp#L710
        public void SerializeRenderItem(FAssetArchive Ar, bool bHasVertexColors, byte numVertexColorChannels)
        {
            var stripDataFlags = Ar.Read<FStripDataFlags>();
            var bIsLODCookedOut = false;
            if (Ar.Game != EGame.GAME_Splitgate)
                bIsLODCookedOut = Ar.ReadBoolean();
            var bInlined = Ar.ReadBoolean();

            RequiredBones = Ar.ReadArray<short>();
            if (!stripDataFlags.IsDataStrippedForServer() && !bIsLODCookedOut)
            {
                Sections = new FSkelMeshSection[Ar.Read<int>()];
                for (var i = 0; i < Sections.Length; i++)
                {
                    Sections[i] = new FSkelMeshSection();
                    Sections[i].SerializeRenderItem(Ar);
                }

                ActiveBoneIndices = Ar.ReadArray<short>();

                if (Ar.Game == EGame.GAME_KenaBridgeofSpirits)
                    Ar.ReadArray<byte>(); // EAssetType_array1

                Ar.Position += 4; //var buffersSize = Ar.Read<uint>();

                if (bInlined)
                {
                    SerializeStreamedData(Ar, bHasVertexColors);
                    if (Ar.Game == EGame.GAME_RogueCompany)
                    {
                        Ar.Position += 12; // 1 (Long) + 2^16 (Int)
                        var elementSize = Ar.Read<int>();
                        var elementCount = Ar.Read<int>();
                        if (elementSize > 0 && elementCount > 0)
                            Ar.SkipBulkArrayData();
                    }
                }
                else
                {
                    var bulk = new FByteBulkData(Ar);
                    if (bulk.Header.ElementCount > 0)
                    {
                        using (var tempAr = new FByteArchive("LodReader", bulk.Data, Ar.Versions))
                        {
                            SerializeStreamedData(tempAr, bHasVertexColors);
                        }

                        var skipBytes = 5;
                        if (FUE5ReleaseStreamObjectVersion.Get(Ar) < FUE5ReleaseStreamObjectVersion.Type.RemovingTessellation && !stripDataFlags.IsClassDataStripped((byte) EClassDataStripFlag.CDSF_AdjacencyData))
                            skipBytes += 5;
                        skipBytes += 4 * 4 + 2 * 4 + 2 * 4;
                        skipBytes += FSkinWeightVertexBuffer.MetadataSize(Ar);
                        Ar.Position += skipBytes;

                        if (HasClothData())
                        {
                            var clothIndexMapping = Ar.ReadArray<long>();
                            Ar.Position += 2 * 4;
                            if (FUE5ReleaseStreamObjectVersion.Get(Ar) >= FUE5ReleaseStreamObjectVersion.Type.AddClothMappingLODBias)
                            {
                                Ar.Position += 4 * clothIndexMapping.Length;
                            }
                        }

                        var profileNames = Ar.ReadArray(Ar.ReadFName);
                    }
                }
            }

            if (Ar.Game == EGame.GAME_ReadyOrNot)
                Ar.Position += 4;
        }

        public void SerializeRenderItem_Legacy(FAssetArchive Ar, bool bHasVertexColors, byte numVertexColorChannels)
        {
            var stripDataFlags = Ar.Read<FStripDataFlags>();

            Sections = new FSkelMeshSection[Ar.Read<int>()];
            for (var i = 0; i < Sections.Length; i++)
            {
                Sections[i] = new FSkelMeshSection();
                Sections[i].SerializeRenderItem(Ar);
            }

            Indices = new FMultisizeIndexContainer(Ar);
            VertexBufferGPUSkin = new FSkeletalMeshVertexBuffer {bUseFullPrecisionUVs = true};

            ActiveBoneIndices = Ar.ReadArray<short>();
            RequiredBones = Ar.ReadArray<short>();

            if (!stripDataFlags.IsDataStrippedForServer() && !stripDataFlags.IsClassDataStripped((byte) EClassDataStripFlag.CDSF_MinLodData))
            {
                var positionVertexBuffer = new FPositionVertexBuffer(Ar);
                var staticMeshVertexBuffer = new FStaticMeshVertexBuffer(Ar);
                var skinWeightVertexBuffer = new FSkinWeightVertexBuffer(Ar, VertexBufferGPUSkin.bExtraBoneInfluences);

                if (!bHasVertexColors && Ar.Game == EGame.GAME_Borderlands3)
                {
                    for (var i = 0; i < numVertexColorChannels; i++)
                    {
                        var newColorVertexBuffer = new FColorVertexBuffer(Ar);
                        ColorVertexBuffer = new FSkeletalMeshVertexColorBuffer(newColorVertexBuffer.Data);
                    }
                }
                else if (bHasVertexColors)
                {
                    var newColorVertexBuffer = new FColorVertexBuffer(Ar);
                    ColorVertexBuffer = new FSkeletalMeshVertexColorBuffer(newColorVertexBuffer.Data);
                }

                if (!stripDataFlags.IsClassDataStripped((byte) EClassDataStripFlag.CDSF_AdjacencyData))
                    AdjacencyIndexBuffer = new FMultisizeIndexContainer(Ar);

                if (HasClothData())
                    ClothVertexBuffer = new FSkeletalMeshVertexClothBuffer(Ar);

                NumVertices = positionVertexBuffer.NumVertices;
                NumTexCoords = staticMeshVertexBuffer.NumTexCoords;

                VertexBufferGPUSkin.VertsFloat = new FGPUVertFloat[NumVertices];
                for (var i = 0; i < VertexBufferGPUSkin.VertsFloat.Length; i++)
                {
                    VertexBufferGPUSkin.VertsFloat[i] = new FGPUVertFloat
                    {
                        Pos = positionVertexBuffer.Verts[i],
                        Infs = skinWeightVertexBuffer.Weights[i],
                        Normal = staticMeshVertexBuffer.UV[i].Normal,
                        UV = staticMeshVertexBuffer.UV[i].UV
                    };
                }
            }

            if (Ar.Game >= EGame.GAME_UE4_23)
            {
                var skinWeightProfilesData = new FSkinWeightProfilesData(Ar);
            }
        }

        private void SerializeStreamedData(FArchive Ar, bool bHasVertexColors)
        {
            var stripDataFlags = Ar.Read<FStripDataFlags>();

            Indices = new FMultisizeIndexContainer(Ar);
            VertexBufferGPUSkin = new FSkeletalMeshVertexBuffer {bUseFullPrecisionUVs = true};

            var positionVertexBuffer = new FPositionVertexBuffer(Ar);
            var staticMeshVertexBuffer = new FStaticMeshVertexBuffer(Ar);
            var skinWeightVertexBuffer = new FSkinWeightVertexBuffer(Ar, VertexBufferGPUSkin.bExtraBoneInfluences);

            if (bHasVertexColors)
            {
                var newColorVertexBuffer = new FColorVertexBuffer(Ar);
                ColorVertexBuffer = new FSkeletalMeshVertexColorBuffer(newColorVertexBuffer.Data);
            }

            if (FUE5ReleaseStreamObjectVersion.Get(Ar) < FUE5ReleaseStreamObjectVersion.Type.RemovingTessellation && !stripDataFlags.IsClassDataStripped((byte) EClassDataStripFlag.CDSF_AdjacencyData))
                AdjacencyIndexBuffer = new FMultisizeIndexContainer(Ar);

            if (HasClothData())
                ClothVertexBuffer = new FSkeletalMeshVertexClothBuffer(Ar);

            var skinWeightProfilesData = new FSkinWeightProfilesData(Ar);

            // Note 07/2021: This was added in UE4.27, but we're only reading it on UE5 for compatibility with Fortnite
            // Note 08/2022: This is more annoying than useful, old fortnite users will need to add a custom option
            if (Ar.Versions["SkeletalMesh.HasRayTracingData"])
            {
                var rayTracingData = Ar.ReadArray<byte>();
            }

            NumVertices = positionVertexBuffer.NumVertices;
            NumTexCoords = staticMeshVertexBuffer.NumTexCoords;

            VertexBufferGPUSkin.VertsFloat = new FGPUVertFloat[NumVertices];
            for (var i = 0; i < VertexBufferGPUSkin.VertsFloat.Length; i++)
            {
                VertexBufferGPUSkin.VertsFloat[i] = new FGPUVertFloat
                {
                    Pos = positionVertexBuffer.Verts[i],
                    Infs = skinWeightVertexBuffer.Weights[i],
                    Normal = staticMeshVertexBuffer.UV[i].Normal,
                    UV = staticMeshVertexBuffer.UV[i].UV
                };
            }
        }

        private bool HasClothData()
        {
            for (var i = 0; i < Chunks.Length; i++)
                if (Chunks[i].HasClothData) // pre-UE4.13 code
                    return true;
            for (var i = 0; i < Sections.Length; i++) // UE4.13+
                if (Sections[i].HasClothData)
                    return true;
            return false;
        }
    }

    public class FStaticLODModelConverter : JsonConverter<FStaticLODModel>
    {
        public override void WriteJson(JsonWriter writer, FStaticLODModel value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Sections");
            serializer.Serialize(writer, value.Sections);

            // writer.WritePropertyName("Indices");
            // serializer.Serialize(writer, value.Indices);

            // writer.WritePropertyName("ActiveBoneIndices");
            // serializer.Serialize(writer, value.ActiveBoneIndices);

            writer.WritePropertyName("NumVertices");
            writer.WriteValue(value.NumVertices);

            writer.WritePropertyName("NumTexCoords");
            writer.WriteValue(value.NumTexCoords);

            // writer.WritePropertyName("RequiredBones");
            // serializer.Serialize(writer, value.RequiredBones);

            writer.WritePropertyName("VertexBufferGPUSkin");
            serializer.Serialize(writer, value.VertexBufferGPUSkin);

            // writer.WritePropertyName("ColorVertexBuffer");
            // serializer.Serialize(writer, value.ColorVertexBuffer);

            // writer.WritePropertyName("AdjacencyIndexBuffer");
            // serializer.Serialize(writer, value.AdjacencyIndexBuffer);

            if (value.Chunks.Length > 0)
            {
                writer.WritePropertyName("Chunks");
                serializer.Serialize(writer, value.Chunks);

                // writer.WritePropertyName("ClothVertexBuffer");
                // serializer.Serialize(writer, value.ClothVertexBuffer);
            }

            if (value.MeshToImportVertexMap.Length > 0)
            {
                // writer.WritePropertyName("MeshToImportVertexMap");
                // serializer.Serialize(writer, value.MeshToImportVertexMap);

                writer.WritePropertyName("MaxImportVertex");
                serializer.Serialize(writer, value.MaxImportVertex);
            }

            writer.WriteEndObject();
        }

        public override FStaticLODModel ReadJson(JsonReader reader, Type objectType, FStaticLODModel existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
