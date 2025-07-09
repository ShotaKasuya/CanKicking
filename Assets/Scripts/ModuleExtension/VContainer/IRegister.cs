namespace VContainer.ModuleExtension
{
    /// <summary>
    /// コンテナに保管するコンポーネントをまとめるクラスに継承させる
    /// </summary>
    public interface IRegisterable
    {
        public void Register(IContainerBuilder builder);
    }
}