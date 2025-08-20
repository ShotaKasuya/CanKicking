using Interface.Global.UserInterface;
using Interface.InGame.UserInterface;
using R3;
using Structure.Utility.Abstraction;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.InGame.UserInterface.Stop
{
    public class RestartButtonView: AbstractButtonView<string>, IStop_RestartButtonView, IFadeUiView
    {
        protected override string EventValue => SceneManager.GetActiveScene().path;
        public Observable<string> Performed => ButtonSubject;
        public Transform SelfTransform => transform;
        public Transform FadeInPosition => fadeInPosition;
        public Transform FadeOutPosition => fadeOutPosition;

        [SerializeField] private Transform fadeOutPosition;
        [SerializeField] private Transform fadeInPosition;
    }
}