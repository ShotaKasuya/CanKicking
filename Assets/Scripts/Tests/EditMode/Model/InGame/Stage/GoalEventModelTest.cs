using Model.InGame.Stage;
using NUnit.Framework;
using R3;

namespace Tests.EditMode.Model.InGame.Stage
{
    public class GoalEventModelTest
    {
        private GoalEventModel _model;
        private CompositeDisposable _disposable;

        [SetUp]
        public void SetUp()
        {
            _disposable = new CompositeDisposable();
            _model = new GoalEventModel(_disposable);
        }

        [TearDown]
        public void TearDown()
        {
            _disposable.Dispose();
        }

        // テストケース1: GoalEventSubjectにOnNextを発行するとGoalEventから通知される
        [Test]
        public void GoalEvent_IsTriggeredBySubject()
        {
            // Arrange
            var eventFired = false;
            _model.GoalEvent.Subscribe(_ => eventFired = true);

            // Act
            _model.GoalEventSubject.OnNext(Unit.Default);

            // Assert
            Assert.IsTrue(eventFired);
        }
    }
}
