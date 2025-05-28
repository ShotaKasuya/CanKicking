using Adapter.IView.InGame.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.Ui.Normal
{
    public class HeightView: MonoBehaviour, IHeightUiView
    {
        [SerializeField] private Text heightText;
        
        public void SetHeight(float height)
        {
            heightText.text = height.ToString("F2") + "m";
        }
    }
}