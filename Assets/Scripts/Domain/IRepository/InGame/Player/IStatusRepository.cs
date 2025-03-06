using DataUtil.InGame.Player;

namespace Domain.IRepository.InGame.Player
{
    // ステータスを始めとした、静的に保存されるような値を読み出すインターフェースを定義する

    public interface IPlayerKickStatusRepository
    {
        public KickPower KickBasePower { get; }
    }
    
    public interface IKickableSpeedRepository
    {
        public float SqrKickableVelocity { get; }
    }
}