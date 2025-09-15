using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Logic.InGame;
using Interface.Model.Global;
using Interface.View.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Controller.InGame.UserInterface;

/// <summary>
/// 一時停止状態のUIを管理する
/// </summary>
public class StopStateController : UserInterfaceBehaviourBase, IStartable
{
    public StopStateController
    (
        IStopUiView stopUiView,
        IPlayButtonView playButtonView,
        IStop_StageSelectButtonView stageSelectButtonView,
        IStop_RestartButtonView reStartButtonView,
        ILoadPrimarySceneLogic loadPrimarySceneLogic,
        ITimeScaleModel timeScaleModel,
        IGameRestartLogic gameRestartLogic,
        CompositeDisposable compositeDisposable,
        IMutStateType<PlayerStateType> playerState,
        IMutAsyncStateType<UserInterfaceStateType> innerState
    ) : base(UserInterfaceStateType.Stop, innerState)
    {
        PlayButtonView = playButtonView;
        StageSelectButtonView = stageSelectButtonView;
        ReStartButtonView = reStartButtonView;
        StopUiView = stopUiView;
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        TimeScaleModel = timeScaleModel;
        GameRestartLogic = gameRestartLogic;
        CompositeDisposable = compositeDisposable;
        PlayerState = playerState;
    }

    public void Start()
    {
        PlayButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.Play())
            .AddTo(CompositeDisposable);
        StageSelectButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (scene, controller) => controller.Load(scene))
            .AddTo(CompositeDisposable);
        ReStartButtonView.Performed
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (_, controller) => controller.Restart())
            .AddTo(CompositeDisposable);
    }

    public override async UniTask OnEnter(CancellationToken token)
    {
        PlayerState.ChangeState(PlayerStateType.Stopping);
        TimeScaleModel.Execute(TimeCommandType.Stop);
        await StopUiView.Show(token);
    }

    public override async UniTask OnExit(CancellationToken token)
    {
        TimeScaleModel.Undo();
        PlayerState.ChangeState(PlayerStateType.Idle);
        await StopUiView.Hide(token);
    }

    private void Play()
    {
        InnerState.ChangeState(UserInterfaceStateType.Normal);
    }

    private void Load(string sceneName)
    {
        LoadPrimarySceneLogic.ChangeScene(sceneName).Forget();
    }

    private void Restart()
    {
        GameRestartLogic.RestartGame();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IMutStateType<PlayerStateType> PlayerState { get; }
    private IPlayButtonView PlayButtonView { get; }
    private IStopUiView StopUiView { get; }
    private IStop_StageSelectButtonView StageSelectButtonView { get; }
    private IStop_RestartButtonView ReStartButtonView { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private ITimeScaleModel TimeScaleModel { get; }
    private IGameRestartLogic GameRestartLogic { get; }
}