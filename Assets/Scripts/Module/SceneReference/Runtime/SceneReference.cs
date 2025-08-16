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
    [Serializable]
    public class SceneReference
    {
        [SerializeField] private SceneType sceneType;
        [SerializeField, HideInInspector] private string sceneName;
        [SerializeField, HideInInspector] private string scenePath;

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
#endif

        private bool _isLoaded;
        private SceneInstance _sceneInstance;
        public SceneType Type => sceneType;
        public string SceneName => sceneName;
        public string ScenePath => scenePath;

        public SceneReference(SceneType sceneType, string scene)
        {
            this.sceneType = sceneType;
            sceneName = scene;
        }
    }
}