using Domain.IEntity.InGame.Player;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Domain.Entity.InGame.Player
{
    public class ConvertScreenBaseVectorEntity: IConvertScreenBaseVectorEntity
    {
        [BurstCompile]
        public Vector2 Convert(ScreenVectorParams arg)
        {
            var shorter = arg.ScreenWidth > arg.ScreenHeight ? arg.ScreenHeight : arg.ScreenWidth;
            var magnification = arg.InputRatio / shorter;

            var screenBaseVector = arg.InputVector * magnification;

            if (math.length(screenBaseVector) > 1)
            {
                return math.normalize(screenBaseVector);
            }

            return screenBaseVector;
        }
    }
}