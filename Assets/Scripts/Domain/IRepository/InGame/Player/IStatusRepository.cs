namespace Domain.IRepository.InGame.Player
{
    // ステータスを始めとした、静的に保存されるような値を読み出すインターフェースを定義する

    public interface IMutPlayerKickStatusRepository: IPlayerKickStatusRepository
    {
        public void SetKickBasePower(float power);
        public void SetKickMaxPower(float power);
    }
    
    public interface IPlayerKickStatusRepository
    {
        public float KickBasePower { get; }
        public float KickMaxPower { get; }
    }
}