using Interface.InGame.UserInterface;
using Module.SceneReference;
using R3;
using UnityEngine;

namespace View.InGame.UserInterface.Stop
{
    public class StageSelectButtonView: AbstractButtonView<SceneReference>, IStop_StageSelectButtonView
    {
        [SerializeField] private SceneReference sceneReference;
        
        protected override SceneReference EventValue => sceneReference;
        public Observable<SceneReference> Performed => ButtonSubject;
    }
}