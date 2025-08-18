using Model.OutGame.StageSelect;
using NUnit.Framework;

namespace Tests.EditMode.Model.OutGame.StageSelect
{
    public class SelectedStageModelTest
    {
        private SelectedStageModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new SelectedStageModel();
        }

        // テストケース1: 初期状態では選択ステージはnull
        [Test]
        public void InitialState_SelectedStageIsNull()
        {
            Assert.IsNull(_model.SelectedStage);
        }

        // テストケース2: SetSelectedStageでステージ名が正しく設定される
        [Test]
        public void SetSelectedStage_SetsStageNameCorrectly()
        {
            // Arrange
            var stageName = "Stage1-1";

            // Act
            _model.SetSelectedStage(stageName);

            // Assert
            Assert.AreEqual(stageName, _model.SelectedStage);
        }

        // テストケース3: SetSelectedStageでnullも設定できる
        [Test]
        public void SetSelectedStage_CanSetNull()
        {
            // Arrange
            _model.SetSelectedStage("SomeStage"); // 初期値を設定

            // Act
            _model.SetSelectedStage(null);

            // Assert
            Assert.IsNull(_model.SelectedStage);
        }

        // テストケース4: SetSelectedStageで上書きできる
        [Test]
        public void SetSelectedStage_CanBeOverwritten()
        {
            // Arrange
            _model.SetSelectedStage("FirstStage");
            var newStage = "SecondStage";

            // Act
            _model.SetSelectedStage(newStage);

            // Assert
            Assert.AreEqual(newStage, _model.SelectedStage);
        }
    }
}
