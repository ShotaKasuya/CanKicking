using Controller.OutGame.Title;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Model.OutGame;
using Interface.View.Global;
using NUnit.Framework;
using R3;

namespace Tests.EditMode.Controller.OutGame.Title
{
    public class TitleControllerTest
    {
        private class MockTouchView : ITouchView
        {
            private readonly Subject<TouchStartEventArgument> _touchSubject = new();
            public Observable<TouchStartEventArgument> TouchEvent => _touchSubject;

            public Module.Option.Runtime.Option<FingerDraggingInfo> DraggingInfo =>
                Module.Option.Runtime.Option<FingerDraggingInfo>.None();

            public Observable<TouchEndEventArgument> TouchEndEvent => Observable.Empty<TouchEndEventArgument>();

            public void SimulateTouch()
            {
                _touchSubject.OnNext(new TouchStartEventArgument());
            }
        }

        private class MockStartSceneModel : IStartSceneModel
        {
            public string SceneToReturn = "TestScene";
            public string GetStartSceneName() => SceneToReturn;
        }

        private class MockLoadPrimarySceneLogic : ILoadPrimarySceneLogic
        {
            public string CalledScenePath { get; private set; }
            public int CallCount { get; private set; }

            public UniTask ChangeScene(string scenePath)
            {
                CalledScenePath = scenePath;
                CallCount++;
                return UniTask.CompletedTask;
            }
        }

        [Test]
        public void Start_OnTouchEvent_ChangesScene()
        {
            // Arrange
            var mockTouchView = new MockTouchView();
            var mockStartSceneModel = new MockStartSceneModel();
            var mockLoadPrimarySceneLogic = new MockLoadPrimarySceneLogic();
            var compositeDisposable = new CompositeDisposable();

            var titleController = new TitleController(
                mockTouchView,
                mockStartSceneModel,
                mockLoadPrimarySceneLogic,
                compositeDisposable
            );

            // Act
            titleController.Start();
            mockTouchView.SimulateTouch();

            // Assert
            Assert.AreEqual(1, mockLoadPrimarySceneLogic.CallCount, "ChangeScene should be called once.");
            Assert.AreEqual(mockStartSceneModel.SceneToReturn, mockLoadPrimarySceneLogic.CalledScenePath,
                "ChangeScene was not called with the correct scene path.");

            // Cleanup
            compositeDisposable.Dispose();
        }
    }
}