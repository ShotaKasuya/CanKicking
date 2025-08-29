using Interface.Model.Global;
using R3;

namespace Tests.Mock.Controller.Global.UserInterface
{
    public class MockSceneLoadEventModel : ISceneLoadEventModel, ISceneLoadSubjectModel
    {
        private readonly Subject<Unit> _startLoadScene = new();
        private readonly Subject<Unit> _endLoadScene = new();
        public Observable<Unit> StartLoadScene => _startLoadScene;
        public Observable<Unit> EndLoadScene => _endLoadScene;
        public Observable<Unit> BeforeSceneLoad => Observable.Empty<Unit>();
        public Observable<Unit> AfterSceneLoad => Observable.Empty<Unit>();
        public Observable<Unit> BeforeNextSceneActivate => Observable.Empty<Unit>();
        public Observable<Unit> AfterNextSceneActivate => Observable.Empty<Unit>();
        public Observable<Unit> BeforeSceneUnLoad => Observable.Empty<Unit>();
        public Observable<Unit> AfterSceneUnLoad => Observable.Empty<Unit>();

        public void SimulateStartLoadScene() => _startLoadScene.OnNext(Unit.Default);
        public void SimulateEndLoadScene() => _endLoadScene.OnNext(Unit.Default);
        public void InvokeStartLoadScene() => _startLoadScene.OnNext(Unit.Default);
        public void InvokeEndLoadScene() => _endLoadScene.OnNext(Unit.Default);
        public void InvokeBeforeSceneLoad() { }
        public void InvokeAfterSceneLoad() { }
        public void InvokeBeforeNextSceneActivate() { }
        public void InvokeAfterNextSceneActivate() { }
        public void InvokeBeforeSceneUnLoad() { }
        public void InvokeAfterSceneUnLoad() { }
    }
}
