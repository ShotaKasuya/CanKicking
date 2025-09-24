using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interface.Logic.InGame;
using Interface.Model.Global;

namespace Logic.InGame.Primary;

public class SaveLogic : IStoreClearDataLogic
{
    private SaveLogic
    (
        IReadOnlyList<IRepositoryFlushModel> repositoryFlushModel
    )
    {
        SaveTasks = new UniTask[repositoryFlushModel.Count];
        RepositoryFlushModel = repositoryFlushModel;
    }

    public UniTask StoreClearData()
    {
        for (int i = 0; i < RepositoryFlushModel.Count; i++)
        {
            SaveTasks[i] = RepositoryFlushModel[i].Flush();
        }

        return UniTask.WhenAll(SaveTasks);
    }

    private UniTask[] SaveTasks { get; }
    private IReadOnlyList<IRepositoryFlushModel> RepositoryFlushModel { get; }
}