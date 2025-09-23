using System.IO;
using Cysharp.Threading.Tasks;
using MessagePack;
using Module.Option.Runtime;
using UnityEngine;

namespace Module.SaveLoader
{
    public abstract class MessagePackRepository<T> : IRepositoryReader<T>, IRepositoryWriter<T>
    {
        public async UniTask Write(string fileName, T dataTransferObject)
        {
            var fullFilePath = Path.Combine(Application.persistentDataPath, fileName);

            var binData = MessagePackSerializer.Serialize(dataTransferObject);
            await File.WriteAllBytesAsync(fullFilePath, binData);
        }

        public async UniTask<Option<T>> Read(string fileName)
        {
            var fullFilePath = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(fullFilePath))
            {
                return Option<T>.None();
            }

            var binData = await File.ReadAllBytesAsync(fileName);
            var dto = MessagePackSerializer.Deserialize<T>(binData);
            return Option<T>.Some(dto);
        }
    }
}