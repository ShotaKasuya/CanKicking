using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
        [SerializeField] private AssetReference scene;
        [SerializeField, HideInInspector] private string sceneName;

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
#endif

        public string SceneName => sceneName;
        public SceneType Type => sceneType;
        public AssetReference SceneAssetReference => scene;

#if UNITY_EDITOR
        public SceneAsset SceneAsset
        {
            get => sceneAsset;
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