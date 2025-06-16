using Adapter.IView.InGame.Ui;
using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace Adapter.View.InGame.Ui.Normal
{
    public class HeightTmpView : MonoBehaviour, IHeightUiView
    {
        [SerializeField] private TMP_Text heightText;

        public void SetHeight(float height)
        {
            heightText.text = ZString.Format("{0:F2}m", heightText);
        }
    }
}