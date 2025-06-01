
namespace Adapter.IDataStore.Util
{
    public interface IScreenDataStore
    {
        public float MinWidth { get; }
        public float MaxWidth { get; }
        public void StoreWeight(float weight);
    }
}