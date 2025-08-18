using Model.Global.Scene;
using Module.SceneReference.Runtime;
using NUnit.Framework;

namespace Tests.EditMode.Model.Global.Scene
{
    public class PrimarySceneModelTest
    {
        private PrimarySceneModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new PrimarySceneModel();
        }

        // テストケース1: 初期状態では現在のSceneContextはデフォルト値
        [Test]
        public void InitialState_CurrentSceneContextIsDefault()
        {
            Assert.AreEqual(default(SceneContext), _model.GetCurrentSceneContext);
        }

        // テストケース2: ToggleCurrentSceneでSceneContextが設定され、古い値(デフォルト値)が返る
        [Test]
        public void ToggleCurrentScene_FirstTime_SetsContextAndReturnsDefault()
        {
            // Arrange
            var newContext = new SceneContext();

            // Act
            var oldContext = _model.ToggleCurrentScene(newContext);

            // Assert
            Assert.AreEqual(default(SceneContext), oldContext);
            Assert.AreEqual(newContext, _model.GetCurrentSceneContext);
        }

        // テストケース3: ToggleCurrentSceneでSceneContextが交換される
        [Test]
        public void ToggleCurrentScene_SecondTime_SwapsContexts()
        {
            // Arrange
            var context1 = new SceneContext();
            var context2 = new SceneContext();
            _model.ToggleCurrentScene(context1); // 初期設定

            // Act
            var returnedContext = _model.ToggleCurrentScene(context2);

            // Assert
            Assert.AreEqual(context1, returnedContext, "Should return the old context");
            Assert.AreEqual(context2, _model.GetCurrentSceneContext, "Should set the new context");
        }
    }
}
