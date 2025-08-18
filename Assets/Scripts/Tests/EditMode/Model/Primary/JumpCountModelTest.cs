using Model.InGame.Primary;
using NUnit.Framework;
using R3;

namespace Tests.EditMode.Model.Primary
{
    public class JumpCountModelTest
    {
        private JumpCountModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new JumpCountModel();
        }

        // テストケース1: 初期値は0である
        [Test]
        public void InitialValue_IsZero()
        {
            Assert.AreEqual(0, _model.JumpCount.CurrentValue);
        }

        // テストケース2: Incを呼ぶとカウントが1増える
        [Test]
        public void Inc_IncrementsCountByOne()
        {
            // Act
            _model.Inc();

            // Assert
            Assert.AreEqual(1, _model.JumpCount.CurrentValue);
        }

        // テストケース3: 複数回Incを呼んでも正しくカウントされる
        [Test]
        public void Inc_MultipleCalls_IncrementsCountCorrectly()
        {
            // Act
            _model.Inc();
            _model.Inc();
            _model.Inc();

            // Assert
            Assert.AreEqual(3, _model.JumpCount.CurrentValue);
        }

        // テストケース4: Resetを呼ぶとカウントが0に戻る
        [Test]
        public void Reset_ResetsCountToZero()
        {
            // Arrange
            _model.Inc();
            _model.Inc();

            // Act
            _model.Reset();

            // Assert
            Assert.AreEqual(0, _model.JumpCount.CurrentValue);
        }

        // テストケース5: 値の変更が購読者に通知される
        [Test]
        public void JumpCount_OnValueChanged_NotifiesSubscriber()
        {
            // Arrange
            int receivedValue = -1;
            using var subscription = _model.JumpCount.Subscribe(v => receivedValue = v);

            // Act
            _model.Inc();

            // Assert
            Assert.AreEqual(1, receivedValue);

            // Act
            _model.Reset();

            // Assert
            Assert.AreEqual(0, receivedValue);
        }
    }
}
