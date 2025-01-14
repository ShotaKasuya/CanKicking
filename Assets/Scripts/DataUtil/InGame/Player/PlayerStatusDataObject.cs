using UnityEngine;

namespace DataUtil.InGame.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerStatusDataObject), menuName = "ScriptableObject", order = 0)]
    public class PlayerStatusDataObject : ScriptableObject
    {
        public KickPowerData KickPowerData => kickPowerData;

        [SerializeField] private KickPowerData kickPowerData;
    }
}