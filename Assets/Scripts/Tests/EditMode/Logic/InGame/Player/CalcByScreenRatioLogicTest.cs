using Interface.Model.Global;
using Interface.Model.InGame;
using Logic.InGame.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Logic.Player
{
    // テスト用のモッククラスを定義
    public class ScreenScaleModelMock : IScreenScaleModel
    {
        public Vector2 Scale { get; set; }
        public float Width => Scale.x;
        public float Height => Scale.y;
    }

    public class PullLimitModelMock : IPullLimitModel
    {
        public float CancelRatio { get; set; }
        public float MaxRatio { get; set; }
    }

    public class CalcByScreenRatioLogicTest
    {
        private ScreenScaleModelMock _screenScaleModel;
        private PullLimitModelMock _pullLimitModel;
        private CalcByScreenRatioLogic _logic;

        [SetUp]
        public void SetUp()
        {
            // 各テストの前にモックオブジェクトを初期化
            _screenScaleModel = new ScreenScaleModelMock();
            _pullLimitModel = new PullLimitModelMock();
            _logic = new CalcByScreenRatioLogic(_screenScaleModel, _pullLimitModel);
        }

        // テストケース1: 入力ベクトルがゼロの場合、キックパワーもゼロになる
        [Test]
        public void CalcKickPower_WhenInputIsZero_ReturnsZero()
        {
            // Arrange (準備)
            _screenScaleModel.Scale = new Vector2(1000, 1000);
            _pullLimitModel.CancelRatio = 0.1f;
            _pullLimitModel.MaxRatio = 0.8f;
            var input = Vector2.zero;

            // Act (実行)
            var result = _logic.CalcKickPower(input);

            // Assert (検証)
            Assert.AreEqual(Vector2.zero, result);
        }

        // テストケース2: ドラッグ距離がキャンセル範囲内の場合、キックパワーはゼロになる
        [Test]
        public void CalcKickPower_WhenInputLengthIsWithinCancelRatio_ReturnsZero()
        {
            // Arrange
            _screenScaleModel.Scale = new Vector2(1000, 1000); // 画面の短辺は1000
            _pullLimitModel.CancelRatio = 0.2f; // 入力長が200 (1000*0.2) 未満ならキャンセル
            _pullLimitModel.MaxRatio = 0.8f;
            var input = new Vector2(100, 0); // 入力長は100

            // Act
            var result = _logic.CalcKickPower(input);

            // Assert
            Assert.AreEqual(Vector2.zero, result);
        }

        // テストケース3: ドラッグ距離が最大範囲に達した場合、キックパワーは1になる
        [Test]
        public void CalcKickPower_WhenInputLengthIsAtMaxRatio_ReturnsNormalizedDirection()
        {
            // Arrange
            _screenScaleModel.Scale = new Vector2(1000, 1000);
            _pullLimitModel.CancelRatio = 0.2f;
            _pullLimitModel.MaxRatio = 0.8f; // 入力長が800 (1000*0.8) で最大
            var input = new Vector2(800, 0); // 入力長は800

            // Act
            var result = _logic.CalcKickPower(input);

            // Assert
            Assert.AreEqual(1f, result.magnitude, 0.001f); // パワー(大きさ)が1に近いか
            Assert.AreEqual(new Vector2(1, 0), result.normalized); // 方向が正しいか
        }

        // テストケース4: ドラッグ距離が最大範囲を超えた場合でも、キックパワーは1にクランプされる
        [Test]
        public void CalcKickPower_WhenInputLengthExceedsMaxRatio_ReturnsNormalizedDirection()
        {
            // Arrange
            _screenScaleModel.Scale = new Vector2(1000, 1000);
            _pullLimitModel.CancelRatio = 0.2f;
            _pullLimitModel.MaxRatio = 0.8f;
            var input = new Vector2(900, 0); // 入力長は900 (最大値の800を超える)

            // Act
            var result = _logic.CalcKickPower(input);

            // Assert
            Assert.AreEqual(1f, result.magnitude, 0.001f); // パワーは1に抑えられる
            Assert.AreEqual(new Vector2(1, 0), result.normalized);
        }

        // テストケース5: ドラッグ距離がキャンセル範囲と最大範囲の中間の場合、キックパワーは0.5になる
        [Test]
        public void CalcKickPower_WhenInputLengthIsHalfway_ReturnsHalfPower()
        {
            // Arrange
            _screenScaleModel.Scale = new Vector2(1000, 1000);
            _pullLimitModel.CancelRatio = 0.2f; // 200
            _pullLimitModel.MaxRatio = 0.8f; // 800
            // 中間は (200 + 800) / 2 = 500
            var input = new Vector2(0, 500); // 入力長は500

            // Act
            var result = _logic.CalcKickPower(input);

            // Assert
            Assert.AreEqual(0.5f, result.magnitude, 0.001f); // パワーが0.5に近いか
            Assert.AreEqual(new Vector2(0, 1), result.normalized); // 方向が正しいか
        }
    }
}