using Interface.InGame.UserInterface;
using Module.SceneReference.AeLa.Utilities;
using R3;
using UnityEngine;

namespace View.InGame.UserInterface.Stop
{
    public class StageSelectButtonView: AbstractButtonView<string>, IStop_StageSelectButtonView
    {
        [SerializeField] private SceneField sceneReference;
        
        protected override string EventValue => sceneReference;
        public Observable<string> Performed => ButtonSubject;
    }
}