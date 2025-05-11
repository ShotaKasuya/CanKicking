using Domain.IEntity.InGame.Player;
using Unity.Burst;
using Unity.Mathematics;

namespace Domain.Entity.InGame.Player
{
    [BurstCompile]
    public struct IsGroundEntity : IIsGroundedEntity
    {
        public bool IsGround(CheckGroundParams checkGroundParams)
        {
            var up = new float2(0f, 1f); // 上方向ベクトル
            var normal = checkGroundParams.Normal;
            var slopeLimit = checkGroundParams.SlopeLimit;

            // 傾斜角度を取得（法線と上方向とのなす角）
            var angle = math.degrees(math.acos(math.dot(math.normalize(normal), up)));

            // 傾斜角が制限以内なら true
            return angle <= slopeLimit;
        }
    }
}