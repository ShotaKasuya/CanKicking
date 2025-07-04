using Interface.InGame.UserInterface;
using UnityEngine;

namespace View.InGame.UserInterface.Normal
{
    /// <summary>
    /// 引っ張り可能な範囲を表示する
    /// </summary>
    public class PullRangeUiView : MonoBehaviour, IPullRangeView
    {
        [SerializeField] private RangeUiView cancelRange;
        [SerializeField] private RangeUiView validRange;

        public void ShowRange(AimContext aimContext)
        {
            var shorter = Screen.width > Screen.height ? Screen.height : Screen.width;
            cancelRange.Set(aimContext.StartPoint, aimContext.CancelRadius * shorter);
            validRange.Set(aimContext.StartPoint, (aimContext.CancelRadius + aimContext.MaxRadius) * shorter);
        }

        public void HideRange()
        {
            cancelRange.Hide();
            validRange.Hide();
        }
    }
}