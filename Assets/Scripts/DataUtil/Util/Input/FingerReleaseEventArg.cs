using UnityEngine;

namespace DataUtil.Util.Input
{
    public struct FingerReleaseEventArg
    {
        public FingerReleaseEventArg
        (
            Vector2 releasePosition,
            Vector2 fingerDelta
        )
        {
            ReleasePosition = releasePosition;
            FingerDelta = fingerDelta;
        }

        /// <summary>
        /// ワールド座標にする
        /// </summary>
        public Vector2 ReleasePosition { get; }
        public Vector2 FingerDelta { get; }
    }
}