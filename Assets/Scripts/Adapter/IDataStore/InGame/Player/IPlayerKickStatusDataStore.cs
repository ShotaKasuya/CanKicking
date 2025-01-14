namespace Adapter.IDataStore.InGame.Player
{
    public interface IPlayerKickStatusDataStore
    {
        public KickStatus LoadKickStatus(int level);
    }

    public struct KickStatus
    {
        public KickStatus(float basePower, float maxPower)
        {
            KickBasePower = basePower;
            KickMaxPower = maxPower;
        }
        
        public float KickBasePower { get; }
        public float KickMaxPower { get; }
    }
}