using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Global.Scene;
using Module.SceneReference.Runtime;
using NUnit.Framework;

namespace Tests.EditMode.Model.Global.Scene
{
    public class SceneResourcesModelTest
    {
        private SceneResourcesModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new SceneResourcesModel();
        }

        // テストケース1: GetResourceScenesが設定されたSceneFieldのリストを返す
        [Test]
        public void GetResourceScenes_ReturnsCorrectSceneNames()
        {
            // Arrange
            // privateなresourceScenesフィールドをリフレクションで設定
            var sceneFields = new List<SceneField> { new SceneField("Scene1"), new SceneField("Scene2") };
            var field = typeof(SceneResourcesModel).GetField("resourceScenes", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(_model, sceneFields);

            // Act
            var result = _model.GetResourceScenes();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Scene1", result[0]);
            Assert.AreEqual("Scene2", result[1]);
        }

        // テストケース2: PushしたReleaseContextが正しく取得できる
        [Test]
        public void GetSceneReleaseContexts_AfterPush_ReturnsPushedContexts()
        {
            // Arrange
            var context1 = new SceneContext();
            var context2 = new SceneContext();

            // Act
            _model.PushReleaseContext(context1);
            _model.PushReleaseContext(context2);
            var result = _model.GetSceneReleaseContexts().ToArray();

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.Contains(context1, result);
            Assert.Contains(context2, result);
        }

        // テストケース3: 初期状態ではReleaseContextは空
        [Test]
        public void InitialState_ReleaseContextsIsEmpty()
        {
            var result = _model.GetSceneReleaseContexts();
            Assert.IsEmpty(result);
        }
    }
}
