using Adapter.IView.InGame.UI;
using Module.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.UI
{
    [RequireComponent(typeof(Button))]
    public class SceneChangeButton : SceneChangeButtonViewBase
    {
        [SerializeField]
        private SceneReference scene;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            SceneChangeEvent.Invoke(scene.SceneName);
        }
    }
}