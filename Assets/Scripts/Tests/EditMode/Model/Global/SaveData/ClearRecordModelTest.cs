using Model.Global.SaveData;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Model.Global.SaveData
{
    public class ClearRecordModelTest
    {
        private ClearRecordModel _model;
        private const string TestKey = "TestStageRecord";

        [SetUp]
        public void SetUp()
        {
            _model = new ClearRecordModel();
            PlayerPrefs.DeleteKey(TestKey); // テスト前にキーを削除して初期状態を保証
        }

        [TearDown]
        public void TearDown()
        {
            PlayerPrefs.DeleteKey(TestKey); // テスト後にキーを削除して他のテストに影響を与えない
        }

        // テストケース1: データがない場合、LoadはNoneを返す
        [Test]
        public void Load_WhenNoDataExists_ReturnsNone()
        {
            var result = _model.Load(TestKey);
            Assert.IsTrue(result.IsNone);
        }

        // テストケース2: 最初のSaveでデータが保存される
        [Test]
        public void Save_FirstTime_SavesDataCorrectly()
        {
            // Arrange
            // PlayerPrefsにキーが存在しない状態にするため、一度ダミーの値を書き込む
            PlayerPrefs.SetInt(TestKey, 999);

            // Act
            _model.Save(TestKey, 10);
            var result = _model.Load(TestKey);

            // Assert
            Assert.IsTrue(result.IsSome);
            Assert.AreEqual(10, result.Unwrap());
        }

        // テストケース3: より良い記録(小さい値)でデータが更新される
        [Test]
        public void Save_WhenNewRecordIsBetter_UpdatesData()
        {
            // Arrange
            PlayerPrefs.SetInt(TestKey, 20); // 初期記録

            // Act
            _model.Save(TestKey, 15);
            var result = _model.Load(TestKey);

            // Assert
            Assert.AreEqual(15, result.Unwrap());
        }

        // テストケース4: 記録が更新されない場合、データは変わらない
        [Test]
        public void Save_WhenNewRecordIsWorse_DoesNotUpdateData()
        {
            // Arrange
            PlayerPrefs.SetInt(TestKey, 20); // 初期記録

            // Act
            _model.Save(TestKey, 25);
            var result = _model.Load(TestKey);

            // Assert
            Assert.AreEqual(20, result.Unwrap());
        }

        // テストケース5: 同じ記録の場合、データは変わらない
        [Test]
        public void Save_WhenNewRecordIsSame_DoesNotUpdateData()
        {
            // Arrange
            PlayerPrefs.SetInt(TestKey, 20); // 初期記録

            // Act
            _model.Save(TestKey, 20);
            var result = _model.Load(TestKey);

            // Assert
            Assert.AreEqual(20, result.Unwrap());
        }
    }
}
