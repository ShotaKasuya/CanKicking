using Interface.Global.Scene;
using R3;

namespace Model.Global.Scene
{
    public class SceneLoadEventModel : ISceneLoadEventModel, ISceneLoadSubjectModel
    {
        public SceneLoadEventModel
        (
            CompositeDisposable compositeDisposable
        )
        {
            StartSceneLoadSubject = new Subject<Unit>().AddTo(compositeDisposable);
            BeforeSceneLoadSubject = new Subject<Unit>().AddTo(compositeDisposable);
            AfterSceneLoadSubject = new Subject<Unit>().AddTo(compositeDisposable);
            BeforeNextSceneActivateSubject = new Subject<Unit>().AddTo(compositeDisposable);
            AfterNextSceneActivateSubject = new Subject<Unit>().AddTo(compositeDisposable);
            BeforeSceneUnLoadSubject = new Subject<Unit>().AddTo(compositeDisposable);
            AfterSceneUnLoadSubject = new Subject<Unit>().AddTo(compositeDisposable);
            EndSceneLoadSubject = new Subject<Unit>().AddTo(compositeDisposable);
        }

        public Observable<Unit> StartLoadScene => StartSceneLoadSubject;
        public Observable<Unit> BeforeSceneLoad => BeforeSceneLoadSubject;
        public Observable<Unit> AfterSceneLoad => AfterSceneLoadSubject;
        public Observable<Unit> BeforeNextSceneActivate => BeforeNextSceneActivateSubject;
        public Observable<Unit> AfterNextSceneActivate => AfterNextSceneActivateSubject;
        public Observable<Unit> BeforeSceneUnLoad => BeforeSceneUnLoadSubject;
        public Observable<Unit> AfterSceneUnLoad => AfterSceneUnLoadSubject;
        public Observable<Unit> EndLoadScene => EndSceneLoadSubject;

        private Subject<Unit> StartSceneLoadSubject { get; }
        private Subject<Unit> BeforeSceneLoadSubject { get; }
        private Subject<Unit> AfterSceneLoadSubject { get; }
        private Subject<Unit> BeforeNextSceneActivateSubject { get; }
        private Subject<Unit> AfterNextSceneActivateSubject { get; }
        private Subject<Unit> BeforeSceneUnLoadSubject { get; }
        private Subject<Unit> AfterSceneUnLoadSubject { get; }
        private Subject<Unit> EndSceneLoadSubject { get; }

        public void InvokeStartLoadScene()
        {
            StartSceneLoadSubject.OnNext(Unit.Default);
        }

        public void InvokeBeforeSceneLoad()
        {
            BeforeSceneLoadSubject.OnNext(Unit.Default);
        }

        public void InvokeAfterSceneLoad()
        {
            AfterSceneLoadSubject.OnNext(Unit.Default);
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

        public void InvokeEndLoadScene()
        {
            EndSceneLoadSubject.OnNext(Unit.Default);
        }
    }
}