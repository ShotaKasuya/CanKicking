using Interface.Model.InGame;
using Module.Option.Runtime;
using UnityEngine;

namespace Model.InGame.Player
{
    /// <summary>
    /// キックした座標を記録するModel
    /// </summary>
    public class KickPositionModel : IKickPositionModel, IResetableModel
    {
        private  Pose[] PositionBuffer { get; }
        private int _count;
        private int _topIndex;

        private int Capacity => PositionBuffer.Length;

        public KickPositionModel()
        {
            const int capacity = 8;
            PositionBuffer = new Pose[capacity];
            _count = 0;
            _topIndex = 0;
        }

        public void PushPosition(Pose pose)
        {
            // 上書きされる場合、古いデータは消える（リングバッファとしての特性）
            _topIndex = (_topIndex + 1) % Capacity;
            PositionBuffer[_topIndex] = pose;

            if (_count < Capacity)
            {
                _count++;
            }
        }

        public Option<Pose> PopPosition()
        {
            if (_count == 0)
            {
                return Option<Pose>.None();
            }

            var value = PositionBuffer[_topIndex];
            _topIndex = (_topIndex - 1 + Capacity) % Capacity;
            _count--;
            return Option<Pose>.Some(value);
        }

        public void Reset()
        {
            _count = 0;
            _topIndex = 0;
        }
    }
}