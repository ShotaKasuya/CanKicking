using Adapter.IView.InGame.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Adapter.View.InGame.UI
{
    [RequireComponent(typeof(Button))]
    public class SceneReloadButton: SceneChangeButtonViewBase
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            SceneChangeEvent.Invoke(SceneManager.GetActiveScene().name);
        }
    }
}