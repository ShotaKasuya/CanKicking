using Interface.Global.Scene;
using R3;

namespace Model.Global.Scene
{
    public class SceneLoadEventModel : ISceneLoadEventModel
    {
        public SceneLoadEventModel()
        {
            BeforeSceneLoadSubject = new Subject<Unit>();
            BeforeNextSceneActivateSubject = new Subject<Unit>();
            AfterNextSceneActivateSubject = new Subject<Unit>();
            BeforeSceneUnLoadSubject = new Subject<Unit>();
            AfterSceneUnLoadSubject = new Subject<Unit>();
        }

        public Observable<Unit> BeforeSceneLoad => BeforeSceneLoadSubject;
        public Observable<Unit> BeforeNextSceneActivate => BeforeNextSceneActivateSubject;
        public Observable<Unit> AfterNextSceneActivate => AfterNextSceneActivateSubject;
        public Observable<Unit> BeforeSceneUnLoad => BeforeSceneUnLoadSubject;
        public Observable<Unit> AfterSceneUnLoad => AfterSceneUnLoadSubject;

        private Subject<Unit> BeforeSceneLoadSubject { get; }
        private Subject<Unit> BeforeNextSceneActivateSubject { get; }
        private Subject<Unit> AfterNextSceneActivateSubject { get; }
        private Subject<Unit> BeforeSceneUnLoadSubject { get; }
        private Subject<Unit> AfterSceneUnLoadSubject { get; }

        public void InvokeBeforeSceneLoad()
        {
            BeforeSceneLoadSubject.OnNext(Unit.Default);
        }

        public void InvokeBeforeNextSceneActivate()
        {
            BeforeNextSceneActivateSubject.OnNext(Unit.Default);
        }

        public void InvokeAfterNextSceneActivate()
        {
            AfterNextSceneActivateSubject.OnNext(Unit.Default);
        }

        public void InvokeBeforeSceneUnLoad()
        {
            BeforeSceneUnLoadSubject.OnNext(Unit.Default);
        }

        public void InvokeAfterSceneUnLoad()
        {
            AfterSceneUnLoadSubject.OnNext(Unit.Default);
        }
    }
}