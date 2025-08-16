using Interface.InGame.Primary;
using R3;

namespace Model.InGame.Primary
{
    public class JumpCountModel : IJumpCountModel
    {
        private ReactiveProperty<int> InnerJumpCount { get; } = new ReactiveProperty<int>();

        public ReadOnlyReactiveProperty<int> JumpCount => InnerJumpCount;

        public void Inc()
        {
            InnerJumpCount.Value++;
        }

        public void Reset()
        {
            InnerJumpCount.Value = 0;
        }
    }
}