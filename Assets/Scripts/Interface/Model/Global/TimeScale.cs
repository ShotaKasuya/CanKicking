using Structure.Global.TimeScale;

namespace Interface.Model.Global;

/// <summary>
/// 外部のクラスが個のインターフェースを通してtimeScaleを変更する
/// </summary>
public interface ITimeScaleModel
{
    public void Execute(TimeCommandType timeCommand);
    public void Undo();

    public void Reset();
}