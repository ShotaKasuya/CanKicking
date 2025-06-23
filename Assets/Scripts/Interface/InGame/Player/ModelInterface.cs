using Structure.Util;

namespace Interface.InGame.Player
{
    /// <summary>
    /// 地面の検出に関する情報を持つ
    /// </summary>
    public interface IGroundDetectionModel
    {
        public RayCastInfo GroundDetectionInfo { get; }
        public float MaxSlope { get; }
    }
}