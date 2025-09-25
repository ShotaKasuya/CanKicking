using System.IO;
using Cysharp.Threading.Tasks;
using MessagePack;
using Module.Option.Runtime;
using UnityEngine;

namespace Module.Repository
{
    /// <summary>
    /// 継承クラスは基本的にInner関数を呼ぶだけで良い。
    /// `Serialize.Deserialize`失敗時に型情報を得るために、ボイラープレートを敷く
    /// </summary>
    public abstract class MessagePackRepository<T> : IRepositoryReader<T>, IRepositoryWriter<T>
    {
        public abstract UniTask Write(string fileName, T dataTransferObject);

        protected async UniTask InnerWrite(string fileName, T dataTransferObject)
        {
            var fullFilePath = Path.Combine(Application.persistentDataPath, fileName);

            var binData = MessagePackSerializer.Serialize(dataTransferObject);
            await File.WriteAllBytesAsync(fullFilePath, binData);
        }

        public abstract UniTask<Option<T>> Read(string fileName);

        protected async UniTask<Option<T>> InnerRead(string fileName)
        {
            var fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(fullFilePath))
            {
                return Option<T>.None();
            }

            var binData = await File.ReadAllBytesAsync(fullFilePath);
            var dto = MessagePackSerializer.Deserialize<T>(binData);
            return Option<T>.Some(dto);
        }
    }
}