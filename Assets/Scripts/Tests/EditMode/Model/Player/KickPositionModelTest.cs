using Model.InGame.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Model.Player
{
    public class KickPositionModelTest
    {
        private KickPositionModel _model;
        private const int Capacity = 8; // 実装で定義されている容量

        [SetUp]
        public void SetUp()
        {
            _model = new KickPositionModel();
        }

        // テストケース1: 初期状態では何もPopできない
        [Test]
        public void PopPosition_WhenBufferIsEmpty_ReturnsNone()
        {
            // Act
            var result = _model.PopPosition();

            // Assert
            Assert.IsTrue(result.IsNone);
        }

        // テストケース2: Pushした値が正しくPopできる
        [Test]
        public void PopPosition_AfterPush_ReturnsPushedValue()
        {
            // Arrange
            var position = new Vector2(10, 20);
            _model.PushPosition(position);

            // Act
            var result = _model.PopPosition();

            // Assert
            Assert.IsTrue(result.IsSome);
            Assert.AreEqual(position, result.Unwrap());
        }

        // テストケース3: 複数回Push/PopしてもLIFO(後入れ先出し)が維持される
        [Test]
        public void PopPosition_MultiplePushes_ReturnsLastInFirstOut()
        {
            // Arrange
            var pos1 = new Vector2(1, 1);
            var pos2 = new Vector2(2, 2);
            _model.PushPosition(pos1);
            _model.PushPosition(pos2);

            // Act & Assert
            var result1 = _model.PopPosition();
            Assert.AreEqual(pos2, result1.Unwrap());

            var result2 = _model.PopPosition();
            Assert.AreEqual(pos1, result2.Unwrap());
        }

        // テストケース4: Pushした回数以上にはPopできない
        [Test]
        public void PopPosition_AfterPoppingAll_ReturnsNone()
        {
            // Arrange
            var position = new Vector2(10, 20);
            _model.PushPosition(position);

            // Act
            _model.PopPosition(); // 1回Popする
            var result = _model.PopPosition(); // さらにもう1回Popする

            // Assert
            Assert.IsTrue(result.IsNone);
        }

        // テストケース5: 容量を超えてPushした場合、古いデータが上書きされる (リングバッファのテスト)
        [Test]
        public void PushPosition_WhenCapacityExceeded_OverwritesOldestValue()
        {
            // Arrange
            // 容量(8) + 1回 Pushする
            for (int i = 0; i < Capacity + 1; i++)
            {
                _model.PushPosition(new Vector2(i, i));
            }

            // Act & Assert
            // Popすると、最後に追加した値から順に出てくるはず
            // 0番目は上書きされているので、出てこない
            for (int i = Capacity; i > 0; i--)
            {
                var result = _model.PopPosition();
                Assert.IsTrue(result.IsSome);
                Assert.AreEqual(new Vector2(i, i), result.Unwrap(), $"Expected value {i}, but was {result.Unwrap()}");
            }

            // これ以上PopするとNoneになるはず
            var finalResult = _model.PopPosition();
            Assert.IsTrue(finalResult.IsNone);
        }
    }
}
