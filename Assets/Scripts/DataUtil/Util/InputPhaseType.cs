namespace DataUtil.Util
{
    /// <summary>
    /// 存在する意味はあまりないが、
    /// InputSystemのasmdefをいろいろなところに置きたくない
    /// </summary>
    public enum InputPhaseType
    {
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
}