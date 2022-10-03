﻿using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.UE4.Assets.Exports.Animation
{
    public abstract class UAnimSequenceBase : UAnimationAsset
    {
        public float SequenceLength;
        public float RateScale;
        //public FRawCurveTracks RawCurveData; Uncomment when you care about editor AnimSequence assets

        public override void Deserialize(FAssetArchive Ar, long validPos)
        {
            base.Deserialize(Ar, validPos);

            SequenceLength = GetOrDefault<float>(nameof(SequenceLength));
            RateScale = GetOrDefault(nameof(RateScale), 1.0f);
            //RawCurveData = GetOrDefault<FRawCurveTracks>(nameof(RawCurveData));
        }
    }
}