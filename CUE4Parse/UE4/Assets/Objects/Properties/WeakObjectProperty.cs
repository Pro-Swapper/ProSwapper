﻿using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.UE4.Assets.Objects
{
    public class WeakObjectProperty : ObjectProperty
    {
        public WeakObjectProperty(FAssetArchive Ar, ReadType type) : base(Ar, type) { }
    }
}