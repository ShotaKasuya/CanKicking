using System.Collections.Generic;
using Controller.Global.Scene;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using Module.SceneReference.Runtime;
using NUnit.Framework;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Tests.EditMode.Controller.Global.Scene
{
    public class ResourceSceneControllerTest
    {
        // Mocks
        private class MockLoadSceneResourcesLogic : ILoadSceneResourcesLogic
        {
            public bool IsLoadCalled { get; private set; }
            public bool IsUnloadCalled { get; private set; }

            public UniTask LoadResources()
            {
                IsLoadCalled = true;
                return UniTask.CompletedTask;
            }

            public UniTask UnLoadResources()
            {
                IsUnloadCalled = true;
                return UniTask.CompletedTask;
            }
        }

        private class MockSceneLoadEventModel : ISceneLoadEventModel
        {
            private readonly Subject<Unit> _afterSceneUnload = new();
            private readonly Subject<Unit> _beforeSceneLoad = new();
            public Observable<Unit> AfterSceneUnLoad => _afterSceneUnload;
            public Observable<Unit> BeforeSceneLoad => _beforeSceneLoad;
            public Observable<Unit> StartLoadScene => Observable.Empty<Unit>();
            public Observable<Unit> AfterSceneLoad => Observable.Empty<Unit>();
            public Observable<Unit> BeforeNextSceneActivate => Observable.Empty<Unit>();
            public Observable<Unit> AfterNextSceneActivate => Observable.Empty<Unit>();
            public Observable<Unit> BeforeSceneUnLoad => Observable.Empty<Unit>();
            public Observable<Unit> EndLoadScene => Observable.Empty<Unit>();

            public void SimulateAfterSceneUnload() => _afterSceneUnload.OnNext(Unit.Default);
            public void SimulateBeforeSceneLoad() => _beforeSceneLoad.OnNext(Unit.Default);
        }

        private class MockResourceScenesModel : IResourceScenesModel
        {
            public IReadOnlyList<string> GetResourceScenes() => new List<string>();

            public void PushReleaseContext(SceneContext sceneContext)
            {
            }

            public IReadOnlyList<SceneContext> GetSceneReleaseContexts() => new List<SceneContext>();
        }

        private class MockBlockingOperationModel : IBlockingOperationModel
        {
            public int SpawnCount { get; private set; }

            public OperationHandle SpawnOperation(string context)
            {
                SpawnCount++;
                return new OperationHandle();
            }

            public bool IsAnyBlocked() => false;
            public IReadOnlyList<OperationHandle> GetOperationHandles => null;
        }

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