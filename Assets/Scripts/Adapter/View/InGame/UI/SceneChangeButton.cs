using System;
using Adapter.IView.InGame.UI;
using Module.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.UI
{
    [RequireComponent(typeof(Button))]
    public class SceneChangeButton : MonoBehaviour, ISceneChangeEventView
    {
        [SerializeField]
        private SceneReference scene;
        public Action<string> SceneChangeEvent { get; set; }

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