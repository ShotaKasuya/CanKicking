using System;
using Adapter.IView.OutGame.StageSelect;
using Cysharp.Threading.Tasks;
using Domain.IRepository.OutGame;
using Module.Option;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Domain.Controller.OutGame.StageSelect
{
    public class NoneStateController : StageSelectStateBehaviourBase, IStartable, IDisposable
    {
        public NoneStateController
        (
            ISelectedStageView selectedStageView,
            ISelectedStageTextView selectedStageTextView,
            ISelectedStageRepository selectedStageRepository,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.None, stateEntity)
        {
            SelectedStageView = selectedStageView;
            SelectedStageTextView = selectedStageTextView;
            SelectedStageRepository = selectedStageRepository;

            CompositeDisposable = new CompositeDisposable();
        }

        public void Start()
        {
            SelectedStageView.SelectEvent
                .Where(_ => IsInState())
                .Subscribe(x => OnSelect(x).Forget())
                .AddTo(CompositeDisposable);
        }

        public override void OnEnter()
        {
            SelectedStageTextView.ResetStage();
        }

        private async UniTask OnSelect(Option<string> selectedStage)
        {
            if (!selectedStage.TryGetValue(out var stage))
            {
                return;
            }

            SelectedStageRepository.SetSelectedStage(stage);

            await UniTask.DelayFrame(1);
            StateEntity.ChangeState(StageSelectStateType.Some);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private ISelectedStageView SelectedStageView { get; }
        private ISelectedStageTextView SelectedStageTextView { get; }
        private ISelectedStageRepository SelectedStageRepository { get; }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}