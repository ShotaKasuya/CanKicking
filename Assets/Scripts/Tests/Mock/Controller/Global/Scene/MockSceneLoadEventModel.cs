using Interface.Model.Global;
using R3;

namespace Tests.Mock.Controller.Global.Scene
{
    public class MockSceneLoadEventModel : ISceneLoadEventModel
    {
        private readonly Subject<Unit> _afterSceneUnload = new();
        private readonly Subject<Unit> _beforeSceneLoad = new();
        public Observable<Unit> AfterSceneUnLoad => _afterSceneUnload;
        public Observable<Unit> BeforeSceneLoad => _beforeSceneLoad;
        public Observable<Unit> StartLoadScene => Observable.Empty<Unit>();
        public Observable<Unit> AfterSceneLoad => Observable.Empty<Unit>();
        public Observable<Unit> BeforeNextSceneActivate => Observable.Empty<Unit>();
        public Observable<Unit> AfterNextSceneActivate => Observable.Empty<Unit>();
        public Observable<Unit> BeforeSceneUnLoad => Observable.Empty<Unit>();
        public Observable<Unit> EndLoadScene => Observable.Empty<Unit>();

        public void SimulateAfterSceneUnload() => _afterSceneUnload.OnNext(Unit.Default);
        public void SimulateBeforeSceneLoad() => _beforeSceneLoad.OnNext(Unit.Default);
    }
}
