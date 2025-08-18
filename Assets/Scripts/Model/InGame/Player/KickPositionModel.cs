using Interface.InGame.Player;
using Module.Option.Runtime;
using UnityEngine;

namespace Model.InGame.Player
{
    /// <summary>
    /// キックした座標を記録するModel
    /// </summary>
    public class KickPositionModel : IKickPositionModel
    {
        private readonly Vector2[] _buffer;
        private int _count;
        private int _topIndex;

        private int Capacity => _buffer.Length;

        public KickPositionModel()
        {
            const int capacity = 8;
            _buffer = new Vector2[capacity];
            _count = 0;
            _topIndex = 0;
        }

        public void PushPosition(Vector2 position)
        {
            // 上書きされる場合、古いデータは消える（リングバッファとしての特性）
            _topIndex = (_topIndex + 1) % Capacity;
            _buffer[_topIndex] = position;

            if (_count < Capacity)
                _count++;
        }

        public Option<Vector2> PopPosition()
        {
            if (_count == 0)
            {
                return Option<Vector2>.None();
            }

            Vector2 value = _buffer[_topIndex];
            _topIndex = (_topIndex - 1 + Capacity) % Capacity;
            _count--;
            return Option<Vector2>.Some(value);
        }
    }
}