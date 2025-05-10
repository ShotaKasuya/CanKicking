using Adapter.IDataStore.InGame.Player;
using UnityEngine;

namespace Adapter.DataStore.InGame.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerKickStatusDataStore), menuName = "ScriptableObject/PlayerKickStatusDataStore", order = 0)]
    public class PlayerKickStatusDataStore : ScriptableObject, IPlayerKickStatusDataStore
    {
        [SerializeField] private float[] kickPower;
        
        public float LoadKickStatus(int level)
        {
            return kickPower[level];
        }
    }
}