using Adapter.IDataStore.InGame.Player;

namespace Adapter.DataStore.InGame.Player
{
    public class PlayerConstantDataStore: IGroundInfoDataStore
    {
        public float MaxSlope => 30;
    }
}