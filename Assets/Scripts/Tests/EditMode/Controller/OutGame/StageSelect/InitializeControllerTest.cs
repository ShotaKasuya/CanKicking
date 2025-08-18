
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Controller.OutGame.StageSelect;
using Cysharp.Threading.Tasks;
using Interface.OutGame.StageSelect;
using NUnit.Framework;

namespace Tests.EditMode.Controller.OutGame.StageSelect
{
    public class InitializeControllerTest
    {
        // Mocks
        private class MockStageIconFactoryView : IStageIconFactoryView
        {
            public IReadOnlyList<string> CalledWithScenes { get; private set; }
            public UniTask MakeIcons(IReadOnlyList<string> sceneNames)
            {
                CalledWithScenes = sceneNames;
                return UniTask.CompletedTask;
            }
        }

        private class MockStageScenesModel : IStageScenesModel
        {
            public IReadOnlyList<string> SceneList { get; set; } = new List<string>();
        }

        private InitializeController _controller;
        private MockStageIconFactoryView _stageIconFactoryView;
        private MockStageScenesModel _stageScenesModel;

        [SetUp]
        public void SetUp()
        {
            _stageIconFactoryView = new MockStageIconFactoryView();
            _stageScenesModel = new MockStageScenesModel();
            _controller = new InitializeController(_stageIconFactoryView, _stageScenesModel);
        }

        [Test]
        public async Task StartAsync_CallsMakeIconsWithCorrectScenes()
        {
            // Arrange
            var scenes = new List<string> { "Scene1", "Scene2" };
            _stageScenesModel.SceneList = scenes;

            // Act
            await _controller.StartAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(_stageIconFactoryView.CalledWithScenes);
            CollectionAssert.AreEqual(scenes, _stageIconFactoryView.CalledWithScenes);
        }
    }
}
