using R3;

namespace Interface.InGame.Stage;

public interface ICameraZoomModel
{
    public float SetZoomLevel(float level);
    public float GetOrthoSize();

    public float ZoomLevel { get; }
    public float Sensitivity { get; }
}

/// <summary>
/// 落下したと判定されるy座標
/// </summary>
public interface IFallLineModel
{
    public float FallLine { get; }
}

public interface IGoalEventModel
{
    public Observable<Unit> GoalEvent { get; }
}

public interface IGoalEventSubjectModel
{
    public Subject<Unit> GoalEventSubject { get; }
}