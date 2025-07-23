using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace VContainer.ModuleExtension
{
    /// <summary>
    /// コンテナに保管するコンポーネントをまとめるクラスに継承させる
    /// </summary>
    public interface IRegisterable
    {
        public void Register(IContainerBuilder builder);
    }

    public static class Extension
    {
        /// <summary>
        /// スレッドプール上でビルドを行うと、Diagnostics Windowを使用していた場合例外が発生する。
        /// これを避けるためこのメソッドを通してLifetimeScopeをビルドする
        /// </summary>
        public static async UniTask BuildOnThreadPool(this LifetimeScope scope)
        {
#if UNITY_EDITOR
            scope.Build();
#else
            await UniTask.RunOnThreadPool(scope.Build);
#endif
        }
    }
}