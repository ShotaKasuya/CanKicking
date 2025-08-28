using Interface.Logic.InGame;
using Interface.Model.InGame;
using R3;

namespace Tests.EditMode.Mocks
{
    public class MockKickCountModel : IKickCountModel, IResetable
    {
        private readonly ReactiveProperty<int> _kickCount = new(0);
        public ReadOnlyReactiveProperty<int> KickCount => _kickCount;
        public int Count => _kickCount.Value;

        public void Inc()
        {
            _kickCount.Value++;
        }

        public void Reset()
        {
            _kickCount.Value = 0;
        }
    }
}
