using System;
using Adapter.IView.InGame.Ui;
using Adapter.IView.Scene;
using Cysharp.Threading.Tasks;
using Module.StateMachine;
using R3;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Domain.Controller.InGame
{
    /// <summary>
    /// ゴール到達後のUIを管理する
    /// </summary>
    public class GoalStateController : UserInterfaceBehaviourBase, IStartable, IDisposable
    {
        public GoalStateController
        (
            IGoalUiView goalUiView,
            IGoalStateReStartButtonView reStartButtonView,
            IGoalStateStageSelectButtonView stageSelectButtonView,
            ISceneLoadView sceneLoadView,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Goal, stateEntity)
        {
            GoalUiView = goalUiView;
            ReStartButtonView = reStartButtonView;
            StageSelectButtonView = stageSelectButtonView;
            SceneLoadView = sceneLoadView;
            CompositeDisposable = new CompositeDisposable();
        }

        public void Start()
        {
            ReStartButtonView.Performed
                .Where(_ => IsInState())
                .Subscribe(sceneName => SceneLoadView.Load(sceneName))
                .AddTo(CompositeDisposable);
            StageSelectButtonView.Performed
                .Where(_ => IsInState())
                .Subscribe(sceneName => SceneLoadView.Load(sceneName))
                .AddTo(CompositeDisposable);
        }

        public override void OnEnter()
        {
            GoalUiView.Show().Forget();
        }

        private IGoalUiView GoalUiView { get; }
        private IGoalStateStageSelectButtonView StageSelectButtonView { get; }
        private IGoalStateReStartButtonView ReStartButtonView { get; }
        private ISceneLoadView SceneLoadView { get; }
        private CompositeDisposable CompositeDisposable { get; }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}