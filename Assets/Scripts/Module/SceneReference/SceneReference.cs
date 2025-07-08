using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Module.SceneReference
{
    public enum SceneType
    {
        Local,
        Addressables,
    }

    [Serializable]
    public class SceneReference
    {
        [SerializeField] private SceneType sceneType;
        [SerializeField, HideInInspector] private string sceneName;

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
#endif

        public SceneType SceneType => sceneType;
        public string SceneName => sceneName;

        public void Load()
        {
            switch (sceneType)
            {
                case SceneType.Local:
                    SceneManager.LoadScene(sceneName);
                    break;
                case SceneType.Addressables:
                    Addressables.LoadSceneAsync(sceneName).WaitForCompletion();
                    break;
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

        public SceneReference(string scene)
        {
            sceneName = scene;
        }
    }
}