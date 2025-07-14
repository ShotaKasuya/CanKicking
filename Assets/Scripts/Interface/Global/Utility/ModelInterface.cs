namespace Interface.Global.Utility;

public interface IBlockingOperationModel
{
    public OperationHandle SpawnOperation(string context);
    public bool IsAnyBlocked();
}

public class OperationHandle
{
    public void Start(string context)
    {
        IsEnd = false;
        OperationContext = context;
    }

    public void Release()
    {
        IsEnd = true;
        OperationContext = string.Empty;
    }

    public string OperationContext { get; private set; }
    public bool IsEnd { get; private set; }

    public OperationHandle
    (
    )
    {
        OperationContext = string.Empty;
        IsEnd = true;
    }
}