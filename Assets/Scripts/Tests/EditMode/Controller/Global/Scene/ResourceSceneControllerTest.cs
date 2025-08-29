using Controller.Global.Scene;
using NUnit.Framework;
using R3;
using Tests.Mock.Controller.Global.Scene;
using UnityEngine;
using VContainer.Unity;

namespace Tests.EditMode.Controller.Global.Scene
{
    public class ResourceSceneControllerTest
    {
        

        private ResourceSceneController _controller;
        private MockLoadSceneResourcesLogic _loadLogic;
        private MockSceneLoadEventModel _eventModel;
        private MockBlockingOperationModel _blockingOperationModel;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            var parentLifetimeScope = new GameObject().AddComponent<LifetimeScope>();
            _loadLogic = new MockLoadSceneResourcesLogic();
            _eventModel = new MockSceneLoadEventModel();
            var resourceScenesModel = new MockResourceScenesModel();
            _blockingOperationModel = new MockBlockingOperationModel();
            _compositeDisposable = new CompositeDisposable();

            _controller = new ResourceSceneController(
                parentLifetimeScope, _loadLogic, _eventModel, resourceScenesModel,
                _blockingOperationModel, _compositeDisposable
            );
            _controller.Initialize();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void OnAfterSceneUnload_LoadsResources()
        {
            _eventModel.SimulateAfterSceneUnload();
            Assert.IsTrue(_loadLogic.IsLoadCalled);
            Assert.AreEqual(1, _blockingOperationModel.SpawnCount);
        }

        [Test]
        public void OnBeforeSceneLoad_UnloadsResources()
        {
            _eventModel.SimulateBeforeSceneLoad();
            Assert.IsTrue(_loadLogic.IsUnloadCalled);
            Assert.AreEqual(1, _blockingOperationModel.SpawnCount);
        }
    }
}