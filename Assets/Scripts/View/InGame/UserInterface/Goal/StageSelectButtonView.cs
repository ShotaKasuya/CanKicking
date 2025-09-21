using Interface.View.InGame.UserInterface;
using Module.SceneReference.Runtime;
using R3;
using UnityEngine;
using View.Utility;

namespace View.InGame.UserInterface.Goal
{
    public class StageSelectButtonView : AbstractButtonView<string>, IGoal_StageSelectButtonView
    {
        [SerializeField] private SceneField sceneField;

        protected override string EventValue => sceneField;
        public Observable<string> Performed => ButtonSubject;
    }
}