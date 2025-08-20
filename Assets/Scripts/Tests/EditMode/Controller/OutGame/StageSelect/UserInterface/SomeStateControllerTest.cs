using System;
using Controller.OutGame.StageSelect.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using Interface.OutGame.StageSelect;
using Module.Option.Runtime;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using System.Threading.Tasks;
using Structure.OutGame;

namespace Tests.EditMode.Controller.OutGame.StageSelect.UserInterface
{
    public class SomeStateControllerTest
    {
        // Mocks
        private class MockLoadPrimarySceneLogic : ILoadPrimarySceneLogic
        {
            public string CalledScenePath { get; private set; }

            public UniTask ChangeScene(string scenePath)
            {
                CalledScenePath = scenePath;
                return UniTask.CompletedTask;
            }
        }

        private class MockStageSelectionView : IStageSelectionView
        {
            private readonly Subject<Option<string>> _subject = new();
            public Observable<Option<string>> SelectEvent => _subject;
            public void SimulateSelect(Option<string> stage) => _subject.OnNext(stage);
        }

        private class MockSelectedStageView : ISelectedStageView
        {
            public string ShownStageName { get; private set; }
            public Option<int> ShownClearRecord { get; private set; }

            public void Reset()
            {
            }

            public void ShowStage(string sceneName, Option<int> clearRecord)
            {
                ShownStageName = sceneName;
                ShownClearRecord = clearRecord;
            }
        }

        private class MockSelectedStageModel : ISelectedStageModel
        {
            public string SelectedStage { get; private set; }
            public void SetSelectedStage(string scene) => SelectedStage = scene;
        }

        private class MockClearRecordModel : IClearRecordModel
        {
            private readonly System.Collections.Generic.Dictionary<string, int> _records = new();
            public void Save(string key, int jumpCount) => _records[key] = jumpCount;

            public Option<int> Load(string key) =>
                _records.TryGetValue(key, out var val) ? Option<int>.Some(val) : Option<int>.None();

            public void AddRecord(string key, int record) => _records[key] = record;
        }

        private class MockStateEntity : IMutStateEntity<StageSelectStateType>
        {
            public StageSelectStateType CurrentState { get; private set; }
            public StageSelectStateType EntryState { get; }
            public Observable<StageSelectStateType> StateExitObservable => _stateExitSubject;
            public Observable<StageSelectStateType> StateEnterObservable => _stateEnterSubject;

            private readonly Subject<StageSelectStateType> _stateExitSubject = new();
            private readonly Subject<StageSelectStateType> _stateEnterSubject = new();

            public MockStateEntity(StageSelectStateType initialState)
            {
                CurrentState = initialState;
                EntryState = initialState;
            }

            public bool IsInState(StageSelectStateType state)
            {
                return CurrentState == state;
            }

            public UniTask ChangeState(StageSelectStateType next)
            {
                _stateExitSubject.OnNext(CurrentState);
                CurrentState = next;
                _stateEnterSubject.OnNext(next);
                return UniTask.CompletedTask;
            }

            public OperationHandle GetStateLock(string context)
            {
                throw new NotImplementedException();
            }
        }

        private SomeStateController _controller;
        private MockLoadPrimarySceneLogic _loadLogic;
        private MockStageSelectionView _selectionView;
        private MockSelectedStageView _selectedStageView;
        private MockSelectedStageModel _selectedStageModel;
        private MockClearRecordModel _clearRecordModel;
        private MockStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _loadLogic = new MockLoadPrimarySceneLogic();
            _selectionView = new MockStageSelectionView();
            _selectedStageView = new MockSelectedStageView();
            _selectedStageModel = new MockSelectedStageModel();
            _clearRecordModel = new MockClearRecordModel();
            _stateEntity = new MockStateEntity(StageSelectStateType.Some);
            _compositeDisposable = new CompositeDisposable();

            _controller = new SomeStateController(
                _loadLogic, _selectionView, _selectedStageView, _selectedStageModel,
                _clearRecordModel, _compositeDisposable, _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void OnEnter_ShowsStageWithRecord()
        {
            const string stageName = "TestStage";
            const int record = 10;
            _selectedStageModel.SetSelectedStage(stageName);
            _clearRecordModel.AddRecord(stageName, record);

            _controller.OnEnter();

            Assert.AreEqual(stageName, _selectedStageView.ShownStageName);
            Assert.IsTrue(_selectedStageView.ShownClearRecord.IsSome);
            Assert.AreEqual(record, _selectedStageView.ShownClearRecord.Unwrap());
        }

        [Test]
        public async Task OnSelect_SameStage_ChangesScene()
        {
            const string stageName = "TestStage";
            _selectedStageModel.SetSelectedStage(stageName);

            _selectionView.SimulateSelect(Option<string>.Some(stageName));

            await UniTask.Yield();

            Assert.AreEqual(stageName, _loadLogic.CalledScenePath);
        }

        [Test]
        public async Task OnSelect_DifferentStage_UpdatesModelAndShowsNewStage()
        {
            const string initialStage = "InitialStage";
            const string newStage = "NewStage";
            _selectedStageModel.SetSelectedStage(initialStage);

            _selectionView.SimulateSelect(Option<string>.Some(newStage));

            await UniTask.Yield();

            Assert.AreEqual(newStage, _selectedStageModel.SelectedStage);
            Assert.AreEqual(newStage, _selectedStageView.ShownStageName);
            Assert.AreEqual(StageSelectStateType.Some, _stateEntity.CurrentState);
        }

        [Test]
        public async Task OnSelect_None_ChangesStateToNone()
        {
            _selectionView.SimulateSelect(Option<string>.None());
            
            await UniTask.Yield();

            Assert.AreEqual(StageSelectStateType.None, _stateEntity.CurrentState);
        }
    }
}