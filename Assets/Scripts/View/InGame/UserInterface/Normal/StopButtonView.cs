using Interface.InGame.UserInterface;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    public class StopButtonView : MonoBehaviour, IStopButtonView
    {
        public Observable<Unit> Performed => stopButton.OnClickAsObservable();

        [SerializeField] private Button stopButton;

        private void Awake()
        {
            Performed.Subscribe(_ => Debug.Log("on click")).AddTo(this);
        }
    }
}