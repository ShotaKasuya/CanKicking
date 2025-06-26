using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Module.SceneReference
{
    [Serializable]
    public class SceneReference
    {
        [SerializeField, HideInInspector] private string sceneName;

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
#endif

        public string SceneName => sceneName;

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