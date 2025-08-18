using Model.Global.Scene;
using NUnit.Framework;
using R3;

namespace Tests.EditMode.Model.Global.Scene
{
    public class SceneLoadEventModelTest
    {
        private SceneLoadEventModel _model;
        private CompositeDisposable _disposable;

        [SetUp]
        public void SetUp()
        {
            _disposable = new CompositeDisposable();
            _model = new SceneLoadEventModel(_disposable);
        }

        [TearDown]
        public void TearDown()
        {
            _disposable.Dispose();
        }

        // 各イベントが正しく発行されるかをテストする
        [Test]
        public void Invoke_TriggersCorrectObservable()
        {
            var startFired = false;
            var beforeLoadFired = false;
            var afterLoadFired = false;
            var beforeActivateFired = false;
            var afterActivateFired = false;
            var beforeUnloadFired = false;
            var afterUnloadFired = false;
            var endFired = false;

            _model.StartLoadScene.Subscribe(_ => startFired = true);
            _model.BeforeSceneLoad.Subscribe(_ => beforeLoadFired = true);
            _model.AfterSceneLoad.Subscribe(_ => afterLoadFired = true);
            _model.BeforeNextSceneActivate.Subscribe(_ => beforeActivateFired = true);
            _model.AfterNextSceneActivate.Subscribe(_ => afterActivateFired = true);
            _model.BeforeSceneUnLoad.Subscribe(_ => beforeUnloadFired = true);
            _model.AfterSceneUnLoad.Subscribe(_ => afterUnloadFired = true);
            _model.EndLoadScene.Subscribe(_ => endFired = true);

            _model.InvokeStartLoadScene();
            Assert.IsTrue(startFired, "StartLoadScene should be fired");

            _model.InvokeBeforeSceneLoad();
            Assert.IsTrue(beforeLoadFired, "BeforeSceneLoad should be fired");

            _model.InvokeAfterSceneLoad();
            Assert.IsTrue(afterLoadFired, "AfterSceneLoad should be fired");

            _model.InvokeBeforeNextSceneActivate();
            Assert.IsTrue(beforeActivateFired, "BeforeNextSceneActivate should be fired");

            _model.InvokeAfterNextSceneActivate();
            Assert.IsTrue(afterActivateFired, "AfterNextSceneActivate should be fired");

            _model.InvokeBeforeSceneUnLoad();
            Assert.IsTrue(beforeUnloadFired, "BeforeSceneUnLoad should be fired");

            _model.InvokeAfterSceneUnLoad();
            Assert.IsTrue(afterUnloadFired, "AfterSceneUnLoad should be fired");

            _model.InvokeEndLoadScene();
            Assert.IsTrue(endFired, "EndLoadScene should be fired");
        }
    }
}
