using Adapter.IDataStore.InGame.Player;
using DataUtil.InGame.Player;
using UnityEngine;

namespace Detail.DataStore.InGame.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerKickStatusDataStore), menuName = "ScriptableObject/PlayerKickStatusDataStore", order = 0)]
    public class PlayerKickStatusDataStore : ScriptableObject, IPlayerKickStatusDataStore, IKickableSpeedDataStore
    {
        [SerializeField] private KickPowerData kickPowerData;
        [SerializeField] private float kickableSpeed;
        
        public KickPower LoadKickStatus(int level)
        {
            return kickPowerData.GetPower(level);
        }

        public float KickableSpeed => kickableSpeed;
    }
}