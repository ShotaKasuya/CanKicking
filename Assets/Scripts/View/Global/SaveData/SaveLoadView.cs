using System.IO;
using Cysharp.Threading.Tasks;
using Interface.View.Global;
using MessagePack;
using Structure.Global;
using UnityEngine;
using View.Utility;

namespace View.Global.SaveData
{
    public class SaveLoadUserDataView : ISaveView<UserState>, ILoadView<UserState>
    {
        public async UniTask Save(UserState data)
        {
            var fileName = Path.Combine(Application.persistentDataPath, SaveDataConstant.UserDataFile);
            var dto = data.Convert();

            var binData = MessagePackSerializer.Serialize(dto);
            await File.WriteAllBytesAsync(fileName, binData);
        }

        public async UniTask<UserState> Load()
        {
            var fileName = Path.Combine(Application.persistentDataPath, SaveDataConstant.UserDataFile);
            if (!File.Exists(fileName))
            {
                return new UserState(GameState.Tutorial, string.Empty, new());
            }

            var binData = await File.ReadAllBytesAsync(fileName);
            var dto = MessagePackSerializer.Deserialize<UserStateDto>(binData);
            return dto.Convert();
        }
    }

    // public class SaveLoadStageDataView : ISaveView<StageData>, ILoadView<StageData>
    // {
    //     public async UniTask Save(StageData data)
    //     {
    //         var dto = data.Convert();
    //         var binData = MessagePackSerializer.Serialize(dto);
    //         await File.WriteAllBytesAsync(fileName, binData);
    //     }
    //
    //     public async UniTask<StageData> Load()
    //     {
    //         if (!File.Exists(fileName))
    //         {
    //             return new StageData();
    //         }
    //
    //         var binData = await File.ReadAllBytesAsync(fileName);
    //         var dto = MessagePackSerializer.Deserialize<StageDto>(binData);
    //         return dto.Convert();
    //     }
    // }
}