using Structure.Utility;

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

    /// <summary>
    /// キックのベースとなる力を持つ
    /// </summary>
    public interface IKickBasePowerModel
    {
        public float BasePower { get; }
    }

    /// <summary>
    /// 画面をどの程度引っ張ったところを最大とするかの比率を持つ
    /// </summary>
    public interface IPullLimitModel
    {
        public float LimitRatio { get; }
    }
}