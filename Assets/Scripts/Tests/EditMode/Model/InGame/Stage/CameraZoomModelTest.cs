using System.Reflection;
using Model.InGame.Stage;
using NUnit.Framework;

namespace Tests.EditMode.Model.InGame.Stage
{
    public class CameraZoomModelTest
    {
        private CameraZoomModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new CameraZoomModel();
            // privateなフィールドをリフレクションで設定
            SetField(_model, "zoomMaxLevel", 20f);
            SetField(_model, "zoomMinLevel", 5f);
        }

        private void SetField(object instance, string fieldName, object value)
        {
            var field = instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(instance, value);
        }

        // テストケース1: ZoomLevelが0-1の範囲にクランプされる
        [Test]
        public void SetZoomLevel_ClampsLevelBetweenZeroAndOne()
        {
            _model.SetZoomLevel(1.5f);
            Assert.AreEqual(1f, _model.ZoomLevel);

            _model.SetZoomLevel(-0.5f);
            Assert.AreEqual(0f, _model.ZoomLevel);
        }

        // テストケース2: ZoomLevelに応じて正しいOrthoSizeが計算される
        [Test]
        [TestCase(0f, 20f)]   // ZoomLevel=0 -> MaxZoom
        [TestCase(1f, 5f)]    // ZoomLevel=1 -> MinZoom
        [TestCase(0.5f, 12.5f)] // ZoomLevel=0.5 -> 中間
        public void GetOrthoSize_CalculatesCorrectlyBasedOnZoomLevel(float zoomLevel, float expectedOrthoSize)
        {
            // Act
            _model.SetZoomLevel(zoomLevel);
            var result = _model.GetOrthoSize();

            // Assert
            Assert.AreEqual(expectedOrthoSize, result, 0.001f);
        }

        // テストケース3: SetZoomLevelが計算後のOrthoSizeを返す
        [Test]
        public void SetZoomLevel_ReturnsCorrectOrthoSize()
        {
            var result = _model.SetZoomLevel(0.25f);
            var expected = 20f * (1 - 0.25f) + 5f * 0.25f; // 15 + 1.25 = 16.25
            Assert.AreEqual(expected, result, 0.001f);
        }
    }
}
