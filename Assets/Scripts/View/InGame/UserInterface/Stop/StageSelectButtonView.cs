using Interface.InGame.UserInterface;
using R3;
using UnityEngine;

namespace View.InGame.UserInterface.Stop
{
    public class StageSelectButtonView: AbstractButtonView<string>, IStop_StageSelectButtonView
    {
        [SerializeField] private string sceneReference;
        
        protected override string EventValue => sceneReference;
        public Observable<string> Performed => ButtonSubject;
    }
}