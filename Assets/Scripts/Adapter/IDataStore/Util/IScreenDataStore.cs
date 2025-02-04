namespace Adapter.IDataStore.Util
{
    public interface IScreenDataStore
    {
        public float ScreenWidth { get; }
        public void ScreenWidthStore(float width);
    }
}