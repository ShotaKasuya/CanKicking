using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace ModuleExtension.VContainer
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
        /// スレッドプール上でビルドを行うと、Diagnostics Windowを使用していた場合、
        /// メインスレッド外からAPIが呼ばれるようで例外が発生する。
        /// これを避けるためこのメソッドを通してLifetimeScopeをビルドする
        /// </summary>
        public static UniTask BuildOnThreadPool(this LifetimeScope scope)
        {
#if UNITY_EDITOR
             scope.Build();
             return UniTask.CompletedTask;
#else
            return UniTask.RunOnThreadPool(scope.Build);
#endif
        }
    }
}