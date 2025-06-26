using Interface.InGame.UserInterface;
using Module.SceneReference;
using R3;
using UnityEngine;
using View.InGame.UserInterface.Stop;

namespace View.InGame.UserInterface.Goal
{
    public class StageSelectButtonView: AbstractButtonView<SceneReference>, IGoal_StageSelectButtonView
    {
        [SerializeField] private SceneReference sceneReference;

        protected override SceneReference EventValue => sceneReference;
        public Observable<SceneReference> Performed => ButtonSubject;
    }
}