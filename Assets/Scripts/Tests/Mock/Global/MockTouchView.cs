using Interface.View.Global;
using Module.Option.Runtime;
using R3;
using UnityEngine;

namespace Tests.Mock.Global
{
    public class MockTouchView : ITouchView
    {
        private readonly Subject<TouchStartEventArgument> _touchSubject = new();
        private readonly Subject<TouchEndEventArgument> _touchEndSubject = new();

        public Observable<TouchStartEventArgument> TouchEvent => _touchSubject;
        public Option<FingerDraggingInfo> DraggingInfo { get; set; } = Option<FingerDraggingInfo>.None();
        public Observable<TouchEndEventArgument> TouchEndEvent => _touchEndSubject;

        public void SimulateTouch(Vector2 position) => _touchSubject.OnNext(new TouchStartEventArgument(position));
        public void SimulateTouchEnd(TouchEndEventArgument arg) => _touchEndSubject.OnNext(arg);
        public void SimulateTouchEnd() => _touchEndSubject.OnNext(new TouchEndEventArgument());
    }
}
