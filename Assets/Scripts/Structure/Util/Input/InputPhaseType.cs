namespace DataUtil.Util.Input
{
    /// <summary>
    /// 存在する意味はあまりないが、
    /// InputSystemのasmdefへの参照をいろいろなところに置きたくない
    /// </summary>
    public enum InputPhaseType
    {
        /// <summary>
        /// 初期レコード
        /// まだ入力がされていない状態
        /// </summary>
        None,
        /// <summary>
        /// タッチが検出されたとき
        /// </summary>
        OnTouch,
        /// <summary>
        /// タッチが移動しているとき
        /// </summary>
        Moving,
        /// <summary>
        /// タッチが静止しているとき
        /// </summary>
        Staying,
        /// <summary>
        /// タッチが離されたとき
        /// </summary>
        OnRelease,
        /// <summary>
        /// システムによってタッチがキャンセルされたとき
        /// </summary>
        SystemCanceled,
    }

    public static class InputExtension
    {
        public static bool IsEvent(this InputPhaseType inputPhase)
        {
            return inputPhase is InputPhaseType.OnTouch or InputPhaseType.OnRelease;
        }
    }
}