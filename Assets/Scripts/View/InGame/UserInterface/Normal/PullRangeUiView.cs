using Interface.InGame.UserInterface;
using TNRD;
using UnityEngine;

namespace View.InGame.UserInterface.Normal
{
    /// <summary>
    /// 引っ張り可能な範囲を表示する
    /// </summary>
    public class PullRangeUiView : MonoBehaviour, IPullRangeView
    {
        [SerializeField] private SerializableInterface<RangeUiView> cancelRange;
        [SerializeField] private SerializableInterface<RangeUiView> validRange;

        public void ShowRange(AimContext aimContext)
        {
            var shorter = Mathf.Min(Screen.width, Screen.height);
            cancelRange.Value.Set(aimContext.StartPoint, shorter * aimContext.CancelRadius);
            validRange.Value.Set(aimContext.StartPoint, shorter * (aimContext.CancelRadius + aimContext.MaxRadius));
        }

        public void HideRange()
        {
            cancelRange.Value.Hide();
            validRange.Value.Hide();
        }
    }
}