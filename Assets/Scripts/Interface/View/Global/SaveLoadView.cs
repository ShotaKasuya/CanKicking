using Cysharp.Threading.Tasks;

namespace Interface.View.Global;

public interface IRepositoryReader<in T>
{
    public UniTask Save(T data);
}

public interface IRepositoryWriter<T>
{
    public UniTask<T> Load();
}