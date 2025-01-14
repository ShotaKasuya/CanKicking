namespace Domain.IRepository.InGame.Player
{
    // リアルタイムに変化するような保存しない値に関連のインターフェースを定義する
    
    public interface IMutKickPowerRepository: IKickPowerRepository
    {
        const float Min = 0;
        const float Max = 1;
        public void SetPower(float power);
    }
    
    public interface IKickPowerRepository
    {
        public float CurrentPower { get; }
    }
}