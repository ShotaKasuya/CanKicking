using DataUtil.InGame.Player;

namespace Adapter.IDataStore.InGame.Player
{
    public interface IPlayerKickStatusDataStore
    {
        public KickPower LoadKickStatus(int level);
    }
    public interface IKickableSpeedDataStore
    {
        public float KickableSpeed { get; }
    }
}