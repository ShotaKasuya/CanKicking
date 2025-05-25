using System;
using Adapter.IView.InGame.Player;
using Adapter.IView.InGame.UI;
using Cysharp.Threading.Tasks;
using Module.StateMachine;
using R3;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Domain.UseCase.InGame.UI
{
    /// <summary>
    /// 通常のUIを管理する
    /// </summary>
    public class NormalStateController : UserInterfaceBehaviourBase, IStartable, IDisposable
    {
        public NormalStateController
        (
            INormalUiView normalUiView,
            IStopEventView stopEventView,
            IHeightUiView heightUiView,
            IPlayerView playerView,
            IGoalEventView goalEventView,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Normal, stateEntity)
        {
            NormalUiView = normalUiView;
            StopEventView = stopEventView;
            GoalEventView = goalEventView;
            HeightUiView = heightUiView;
            PlayerView = playerView;
            CompositeDisposable = new CompositeDisposable();
        }

        public void Start()
        {
            StopEventView.OnPerformed
                .Where(_ => IsInState())
                .Subscribe(_ => ChangeToStop())
                .AddTo(CompositeDisposable);

            GoalEventView.OnPerformed
                .Where(_ => IsInState())
                .Subscribe(_ => ChangeToGoal())
                .AddTo(CompositeDisposable);
        }

        public override void OnEnter()
        {
            NormalUiView.Show().Forget();
        }

        public override void OnExit()
        {
            NormalUiView.Hide().Forget();
        }

        public override void StateUpdate(float deltaTime)
        {
            var height = PlayerView.PlayerPose.position.y;
            HeightUiView.SetHeight(height);
        }

        private void ChangeToGoal()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Goal);
        }

        private void ChangeToStop()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Stop);
        }

        private INormalUiView NormalUiView { get; }
        private IStopEventView StopEventView { get; }
        private IGoalEventView GoalEventView { get; }
        private IHeightUiView HeightUiView { get; }
        private IPlayerView PlayerView { get; }
        private CompositeDisposable CompositeDisposable { get; }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}
