using Adapter.IView.InGame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Detail.View.InGame.UI
{
    public class PowerImageView : MonoBehaviour, IPowerView
    {
        [SerializeField] private Image image;
        [SerializeField] private Text text;

        public void SetPower(float power)
        {
            image.fillAmount = power;
            var ipower = (int)(power * 100); // 100分率で表示
            text.text = ipower.ToString();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}