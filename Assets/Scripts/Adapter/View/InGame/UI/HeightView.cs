using Adapter.IView.InGame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Detail.View.InGame.UI
{
    public class HeightView: MonoBehaviour, IHeightView
    {
        [SerializeField] private Text heightText;
        
        public void SetHeight(float height)
        {
            heightText.text = height.ToString("F2") + "m";
        }
    }
}