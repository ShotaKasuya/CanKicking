using Interface.Logic.InGame;
using Interface.Model.InGame;
using Interface.View.InGame;
using Module.Option.Runtime;
using Module.StateMachine;
using Structure.InGame.UserInterface;
using UnityEngine;

namespace Tests.Mock.InGame
{
    // ============================================================================================
    // View
    // ============================================================================================
    public class MockLazyPlayerView : ILazyPlayerView
    {
        public OnceCell<IPlayerView> PlayerView { get; } = new();
    }

    public class MockLazyBaseHeightView : ILazyBaseHeightView
    {
        public OnceCell<float> BaseHeight { get; } = new();
    }

    public class MockLazyGoalHeightView : ILazyGoalHeightView
    {
        public OnceCell<float> GoalHeight { get; } = new();
    }

    // ============================================================================================
    // Model
    // ============================================================================================
    public class MockKickBasePowerModel : IKickBasePowerModel
    {
        public float KickPower { get; set; } = 10f;
        public float RotationPower { get; set; } = 1f;
    }

    public class MockPlayerSoundModel : IPlayerSoundModel
    {
        public AudioClip KickSound = AudioClip.Create("Kick", 1, 1, 1000, false);
        public AudioClip BoundSound = AudioClip.Create("Bound", 1, 1, 1000, false);
        public AudioClip GetKickSound() => KickSound;
        public AudioClip GetBoundSound() => BoundSound;
    }

    // ============================================================================================
    // Logic
    // ============================================================================================
    public class MockCalcKickPowerLogic : ICalcKickPowerLogic
    {
        public Vector2 PowerToReturn { get; set; } = Vector2.one;
        public Vector2 CalcKickPower(Vector2 input) => PowerToReturn;
    }

    public class MockUiStateEntity : AbstractAsyncStateType<UserInterfaceStateType>
    {
        public MockUiStateEntity(UserInterfaceStateType entryState) : base(entryState)
        {
        }
    }
}