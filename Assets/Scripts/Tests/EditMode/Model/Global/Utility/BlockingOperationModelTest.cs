using Model.Global.Utility;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Model.Global.Utility
{
    public class BlockingOperationModelTest
    {
        private BlockingOperationModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new BlockingOperationModel();
        }

        // テストケース1: SpawnOperationで利用可能なハンドルが取得できる
        [Test]
        public void SpawnOperation_WhenHandleIsAvailable_ReturnsAvailableHandle()
        {
            // Act
            var handle = _model.SpawnOperation("Test1");

            // Assert
            Assert.IsNotNull(handle);
            Assert.IsFalse(handle.IsEnd);
        }

        // テストケース2: すべてのハンドルが使用中の場合、新しいハンドルが作成される
        [Test]
        public void SpawnOperation_WhenAllHandlesAreBusy_CreatesNewHandle()
        {
            // Arrange
            var initialCount = _model.GetOperationHandles.Count;
            for (int i = 0; i < initialCount; i++)
            {
                _model.SpawnOperation($"Test{i}");
            }

            // Act
            var newHandle = _model.SpawnOperation("OverflowTest");

            // Assert
            Assert.IsNotNull(newHandle);
            Assert.AreEqual(initialCount + 1, _model.GetOperationHandles.Count);
        }

        // テストケース3: ハンドルを終了させると再利用される
        [Test]
        public void SpawnOperation_ReusesEndedHandles()
        {
            // Arrange
            var handle1 = _model.SpawnOperation("Test1");

            // Act
            handle1.Dispose();
            var handle2 = _model.SpawnOperation("Test2");

            // Assert
            // 同じインスタンスが再利用されることを確認
            Assert.AreSame(handle1, handle2);
            Assert.IsFalse(handle2.IsEnd);
        }

        // テストケース4: IsAnyBlockedが正しく動作する
        [Test]
        public void IsAnyBlocked_ReturnsCorrectState()
        {
            // Assert initial state
            // IsEndがtrueのものを探すので、初期状態ではfalseのはず
            Assert.IsFalse(_model.IsAnyBlocked());

            // Arrange
            var handle = _model.SpawnOperation("Test");
            Debug.Log(_model.GetAllOperations());
            Assert.IsTrue(_model.IsAnyBlocked());

            // Act
            handle.Dispose();

            // Assert
            Debug.Log(_model.GetAllOperations());
            Assert.IsFalse(_model.IsAnyBlocked());
        }
    }
}
