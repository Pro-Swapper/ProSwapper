﻿using System;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Versions;

namespace CUE4Parse.UE4.Objects.UObject
{
    public class UStruct : Assets.Exports.UObject
    {
        public FPackageIndex SuperStruct;
        public FPackageIndex[] Children;
        public FField[] ChildProperties;

        public override void Deserialize(FAssetArchive Ar, long validPos)
        {
            base.Deserialize(Ar, validPos);
            SuperStruct = new FPackageIndex(Ar);
            if (FFrameworkObjectVersion.Get(Ar) < FFrameworkObjectVersion.Type.RemoveUField_Next)
            {
                throw new NotImplementedException();
            }
            else
            {
                Children = Ar.ReadArray(() => new FPackageIndex(Ar));
                
            }

            if (FCoreObjectVersion.Get(Ar) >= FCoreObjectVersion.Type.FProperties)
            {
                DeserializeProperties(Ar);    
            }
            var bytecodeBufferSize = Ar.Read<int>();
            var serializedScriptSize = Ar.Read<int>();
            Ar.Position += serializedScriptSize; // should we read the bytecode some day?
        }

        protected void DeserializeProperties(FAssetArchive Ar)
        {
            ChildProperties = Ar.ReadArray(() =>
            {
                var propertyTypeName = Ar.ReadFName();
                var prop = FField.Construct(propertyTypeName);
                prop.Deserialize(Ar);
                return prop;
            });
        }
    }
}