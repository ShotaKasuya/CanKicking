using Unity.Mathematics;
using UnityEngine;

namespace Domain.IEntity.InGame.Player
{
    public interface IConvertScreenBaseVectorEntity
    {
        public Vector2 Convert(ScreenVectorParams arg);
    }

    public readonly struct ScreenVectorParams
    {
        public float2 InputVector { get; }
        public float InputRatio { get; }
        public float ScreenWidth { get; }
        public float ScreenHeight { get; }

        public ScreenVectorParams
            (
                Vector2 inputVector,
                float inputRatio,
                float screenWidth,
                float screenHeight
                )
        {
            InputVector = inputVector;
            InputRatio = inputRatio;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }
    }
}