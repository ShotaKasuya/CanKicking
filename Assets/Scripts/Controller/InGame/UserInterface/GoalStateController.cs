using System;
using Interface.Global.Scene;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Controller.InGame.UserInterface
{
    /// <summary>
    /// ゴール到達後のUIを管理する
    /// </summary>
    public class GoalStateController : UserInterfaceBehaviourBase, IStartable, IDisposable
    {
        /// <summary>
        /// ゴール到達後のUIを管理する
        /// </summary>
        public GoalStateController
        (
            IGoalUiView goalUiView,
            IGoal_RestartButtonView restartButtonView,
            IGoal_StageSelectButtonView stageSelectButtonView,
            ISceneLoaderView sceneLoadView,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Goal, stateEntity)
        {
            GoalUiView = goalUiView;
            RestartButtonView = restartButtonView;
            StageSelectButtonView = stageSelectButtonView;
            SceneLoaderView = sceneLoadView;
            
            CompositeDisposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            RestartButtonView.Performed
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (reference, controller) => controller.SceneLoaderView.LoadScene(reference))
                .AddTo(CompositeDisposable);
            StageSelectButtonView.Performed
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (reference, controller) => controller.SceneLoaderView.LoadScene(reference))
                .AddTo(CompositeDisposable);
        }
        
        public override void OnEnter()
        {
            GoalUiView.Show();
        }
        
        private IGoalUiView GoalUiView { get; }
        private IGoal_RestartButtonView RestartButtonView { get; }
        private IGoal_StageSelectButtonView StageSelectButtonView { get; }
        private ISceneLoaderView SceneLoaderView { get; }
        private CompositeDisposable CompositeDisposable { get; }
        
        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}