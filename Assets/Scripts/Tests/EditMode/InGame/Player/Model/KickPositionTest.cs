using System.Linq;
using Model.InGame.Player;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.InGame.Player.Model
{
    public class KickPositionTest
    {
        [Test]
        public void PushPopTest()
        {
            var kickPositionModel = new KickPositionModel();
            var positions = new[]
            {
                Vector2.zero, Vector2.one, Vector2.right, Vector2.left,
                Vector2.up, Vector2.down,
            };

            foreach (var position in positions)
            {
                kickPositionModel.PushPosition(new Pose(position,Quaternion.identity));
            }

            foreach (var position in positions.Reverse())
            {
                var popPosition = kickPositionModel.PopPosition();
                Assert.IsTrue(popPosition.TryGetValue(out var value));
                Debug.Log($"position == value: {position} == {value}");
                Assert.IsTrue(position == new Vector2(value.position.x, value.position.y));
            }
        }
    }
}