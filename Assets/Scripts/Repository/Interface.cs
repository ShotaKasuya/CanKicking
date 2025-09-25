using Cysharp.Threading.Tasks;
using Module.Option.Runtime;

namespace Module.Repository
{
    public interface IRepositoryReader<T>
    {
        public UniTask<Option<T>> Read(string fileName);
    }

    public interface IRepositoryWriter<in T>
    {
        public UniTask Write(string fileName, T dto);
    }
}