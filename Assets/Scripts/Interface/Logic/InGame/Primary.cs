using Cysharp.Threading.Tasks;

namespace Interface.Logic.InGame;

public interface IGameRestartLogic
{
    public void RestartGame();
}

public interface IResetable
{
    public void Reset();
}

/// <summary>
/// ゲームクリア時にセーブを行う処理
/// </summary>
public interface IStoreClearDataLogic
{
    public UniTask StoreClearData();
}
