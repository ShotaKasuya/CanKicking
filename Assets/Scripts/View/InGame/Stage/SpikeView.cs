using Interface.View.InGame;
using Structure.InGame.Player;
using UnityEngine;

namespace View.InGame.Stage
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpikeView : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IPlayerCommandReceiver>(out var receiver))
            {
                receiver.SendCommand(new PlayerInteractCommand(
                    CommandType.Undo
                ));
            }
        }
    }
}