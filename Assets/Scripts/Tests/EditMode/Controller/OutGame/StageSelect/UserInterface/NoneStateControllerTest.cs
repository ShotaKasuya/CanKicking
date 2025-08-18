
using System;
using System.Threading.Tasks;
using Controller.OutGame.StageSelect.UserInterface;
using Interface.OutGame.StageSelect;
using Module.Option.Runtime;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.OutGame;

namespace Tests.EditMode.Controller.OutGame.StageSelect.UserInterface
{
    public class NoneStateControllerTest
    {
        // Mocks
        private class MockStageSelectionView : IStageSelectionView
        {
            private readonly Subject<Option<string>> _subject = new();
            public Observable<Option<string>> SelectEvent => _subject;
            public void SimulateSelect(Option<string> stage) => _subject.OnNext(stage);
        }

        private class MockSelectedStageView : ISelectedStageView
        {
            public bool IsReset { get; private set; }
            public void Reset() => IsReset = true;
            public void ShowStage(string sceneName, Option<int> clearRecord) { }
        }

        private class MockSelectedStageModel : ISelectedStageModel
        {
            public string SelectedStage { get; private set; }
            public void SetSelectedStage(string scene) => SelectedStage = scene;
        }

        private class MockStateEntity : IMutStateEntity<StageSelectStateType>
        {
            public StageSelectStateType State { get; private set; } = StageSelectStateType.None;
            public System.Action<StatePair<StageSelectStateType>> OnChangeState { get; set; }
            public bool IsInState(StageSelectStateType state) => State == state;
            public void ChangeState(StageSelectStateType next) => State = next;
        }

        private NoneStateController _controller;
        private MockStageSelectionView _selectionView;
        private MockSelectedStageView _selectedStageView;
        private MockSelectedStageModel _selectedStageModel;
        private MockStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _selectionView = new MockStageSelectionView();
            _selectedStageView = new MockSelectedStageView();
            _selectedStageModel = new MockSelectedStageModel();
            _stateEntity = new MockStateEntity();
            _compositeDisposable = new CompositeDisposable();

            _controller = new NoneStateController(
                _selectionView, _selectedStageView, _selectedStageModel, 
                _compositeDisposable, _stateEntity
            );
            _controller.Start();
        }

        [TearDown] public void TearDown() => _compositeDisposable.Dispose();

        [Test] public void OnEnter_ResetsSelectedStageView() { _controller.OnEnter(); Assert.IsTrue(_selectedStageView.IsReset); }

        [Test]
        public async Task OnSelect_WithStage_SetsModelAndChangesState()
        {
            const string stageName = "TestStage";
            _selectionView.SimulateSelect(Option<string>.Some(stageName));

            await Task.Delay(TimeSpan.FromSeconds(0.25f));
            
            Assert.AreEqual(stageName, _selectedStageModel.SelectedStage);
            Assert.AreEqual(StageSelectStateType.Some, _stateEntity.State);
        }

        [Test]
        public void OnSelect_WithNone_DoesNothing()
        {
            _selectionView.SimulateSelect(Option<string>.None());
            
            Assert.IsNull(_selectedStageModel.SelectedStage);
            Assert.AreEqual(StageSelectStateType.None, _stateEntity.State);
        }
    }
}
