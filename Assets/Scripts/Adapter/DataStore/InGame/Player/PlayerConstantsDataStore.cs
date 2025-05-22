using Adapter.IDataStore.InGame.Player;
using UnityEngine;

namespace Adapter.DataStore.InGame.Player
{
    [CreateAssetMenu(fileName = "PlayerConstants", menuName = "PlayerConstants", order = 0)]
    public class PlayerConstantsDataStore : ScriptableObject, IGroundInfoDataStore, IPlayerKickStatusDataStore
    {
        [SerializeField] private float maxSlope;
        [SerializeField] private float kickPower;
        
        public float MaxSlope => maxSlope;
        public float LoadKickStatus(int level)
        {
            return kickPower;
        }
    }
}