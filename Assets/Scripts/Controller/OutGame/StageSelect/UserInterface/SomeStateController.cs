// namespace Domain.Controller.OutGame.StageSelect
// {
//     public class SomeStateController : StageSelectStateBehaviourBase, IStartable, IDisposable
//     {
//         // public SomeStateController
//         // (
//         //     ISceneLoadView sceneLoadView,
//         //     ISelectedStageView selectedStageView,
//         //     ISelectedStageTextView selectedStageTextView,
//         //     ISelectedStageRepository selectedStageRepository,
//         //     IMutStateEntity<StageSelectStateType> stateEntity
//         // ) : base(StageSelectStateType.Some, stateEntity)
//         // {
//         //     SceneLoadView = sceneLoadView;
//         //     SelectedStageView = selectedStageView;
//         //     SelectedStageTextView = selectedStageTextView;
//         //     SelectedStageRepository = selectedStageRepository;
//         //
//         //     CompositeDisposable = new CompositeDisposable();
//         // }
//         //
//         // public void Start()
//         // {
//         //     SelectedStageView.SelectEvent
//         //         .Where(_ => IsInState())
//         //         .Subscribe(x => OnSelect(x))
//         //         .AddTo(CompositeDisposable);
//         // }
//         //
//         // public override void OnEnter()
//         // {
//         //     var stage = SelectedStageRepository.SelectedStage;
//         //     SelectedStageTextView.SetStage(stage);
//         // }
//         //
//         // private void OnSelect(Option<string> selectedStage)
//         // {
//         //     var prevSelect = SelectedStageRepository.SelectedStage;
//         //     if (!selectedStage.TryGetValue(out var stage))
//         //     {
//         //         StateEntity.ChangeState(StageSelectStateType.None);
//         //         return;
//         //     }
//         //
//         //     if (stage != prevSelect)
//         //     {
//         //         SelectedStageRepository.SetSelectedStage(stage);
//         //         OnEnter();
//         //         return;
//         //     }
//         //
//         //     SceneLoadView.Load(prevSelect);
//         // }
//         //
//         // private CompositeDisposable CompositeDisposable { get; }
//         // private ISelectedStageView SelectedStageView { get; }
//         // private ISelectedStageTextView SelectedStageTextView { get; }
//         // private ISceneLoadView SceneLoadView { get; }
//         // private ISelectedStageRepository SelectedStageRepository { get; }
//         //
//         // public void Dispose()
//         // {
//         //     CompositeDisposable?.Dispose();
//         // }
//     }
// }