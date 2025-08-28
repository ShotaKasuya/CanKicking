using Interface.Model.InGame;
using R3;

namespace Model.InGame.Primary
{
    public class KickCountModel : IKickCountModel, IResetableModel
    {
        private ReactiveProperty<int> InnerKickCount { get; } = new ReactiveProperty<int>();

        public ReadOnlyReactiveProperty<int> KickCount => InnerKickCount;

        public void Inc()
        {
            InnerKickCount.Value++;
        }

        public void Reset()
        {
            InnerKickCount.Value = 0;
        }
    }
}