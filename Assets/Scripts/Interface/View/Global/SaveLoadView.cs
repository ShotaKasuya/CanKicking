using Cysharp.Threading.Tasks;

namespace Interface.View.Global;

public interface ISaveView<in T>
{
    public UniTask Save(T data);
}

public interface ILoadView<T>
{
    public UniTask<T> Load();
}