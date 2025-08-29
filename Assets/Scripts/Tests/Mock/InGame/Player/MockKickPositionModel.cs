using Interface.Model.InGame;
using Module.Option.Runtime;
using UnityEngine;

namespace Tests.Mock.InGame.Player
{
    public class MockKickPositionModel : IKickPositionModel
    {
        private Pose? _positionToPop;
        public Pose? PushedPosition { get; private set; }

        public void SetPositionToPop(Pose? pos) => _positionToPop = pos;

        public Option<Pose> PopPosition() => _positionToPop.HasValue
            ? Option<Pose>.Some(_positionToPop.Value)
            : Option<Pose>.None();

        public void PushPosition(Pose pose)
        {
            PushedPosition = pose;
        }
    }
}
