using Interface.InGame.UserInterface;
using Module.SceneReference.AeLa.Utilities;
using R3;
using UnityEngine;
using View.InGame.UserInterface.Stop;

namespace View.InGame.UserInterface.Goal
{
    public class StageSelectButtonView: AbstractButtonView<string>, IGoal_StageSelectButtonView
    {
        [SerializeField] private SceneField sceneField;

        protected override string EventValue => sceneField;
        public Observable<string> Performed => ButtonSubject;
    }
}