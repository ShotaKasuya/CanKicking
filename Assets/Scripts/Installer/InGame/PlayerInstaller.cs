using DataUtil.InGame.Player;
using Detail.View.InGame.Input;
using Detail.View.InGame.Player;
using UnityEngine;

namespace Installer.InGame
{
    public class PlayerInstaller: MonoBehaviour
    {
        [SerializeField] private PlayerInitialStatus playerInitialStatus;
        
        private void Awake()
        {
            var view = GetComponent<PlayerView>();
            var inputAction = new InputSystem_Actions();

        }
    }
}