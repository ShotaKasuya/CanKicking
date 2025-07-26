using Interface.InGame.Stage;
using Module.Option;
using UnityEngine;

namespace View.InGame.Stage
{
    public class StartPositionView : IStartPositionView
    {
        public Option<Transform> StartPosition => InnerStartPosition.Get();

        public void SetStartPosition(Transform startPosition)
        {
            InnerStartPosition.Init(startPosition);
        }

        private OnceCell<Transform> InnerStartPosition { get; } = new OnceCell<Transform>();
    }
}