using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Module.SceneReference
{
    public enum SceneType
    {
        Local,
        Addressable,
    }

    [Serializable]
    public class SceneReference
    {
        [SerializeField] private SceneType sceneType;
        [SerializeField, HideInInspector] private string sceneName;

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
#endif

        private bool _isLoaded;
        private SceneInstance _sceneInstance;
        public SceneType SceneType => sceneType;
        public string SceneName => sceneName;

        public void Load()
        {
            switch (sceneType)
            {
                case SceneType.Local:
                    SceneManager.LoadScene(sceneName);
                    break;
                case SceneType.Addressable:
                    Addressables.LoadSceneAsync(sceneName).WaitForCompletion();
                    _isLoaded = true;
                    break;
            }
        }

        public void UnLoad()
        {
            if (sceneType == SceneType.Addressable)
            {
                if (!_isLoaded)
                {
                    Debug.LogWarning("trying to unload not loaded scene");
                }

                Addressables.UnloadSceneAsync(_sceneInstance).WaitForCompletion();
            }
        }

#if UNITY_EDITOR
        public SceneAsset SceneAsset
        {
            get => sceneAsset!;
            set
            {
                sceneAsset = value;
                sceneName = sceneAsset != null ? sceneAsset.name : "";
            }
        }
#endif

        public SceneReference(SceneType sceneType, string scene)
        {
            this.sceneType = sceneType;
            sceneName = scene;
        }
    }
}