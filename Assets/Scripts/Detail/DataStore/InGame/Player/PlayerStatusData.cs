using Adapter.IDataStore.InGame.Player;
using DataUtil.InGame.Player;

namespace Detail.DataStore.InGame.Player
{
    public class PlayerStatusData : IPlayerKickStatusDataStore
    {
        public PlayerStatusData
        (
            PlayerStatusDataObject statusDataObject
        )
        {
            PowerData = statusDataObject.KickPowerData;
        }

        public KickStatus LoadKickStatus(int level)
        {
            var data = PowerData.GetPower(level);
            return new KickStatus(
                0,
                data.MaxPower);
        }

        private KickPowerData PowerData { get; }
    }
}