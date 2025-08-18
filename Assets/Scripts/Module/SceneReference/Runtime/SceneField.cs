using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Module.SceneReference.Runtime
{
// Source: http://answers.unity.com/comments/1374414/view.html


    /// <summary>
    /// Unity editor-friendly scene reference field.
    /// </summary>
    [System.Serializable]
    public class SceneField
    {
        [SerializeField] private Object sceneAsset;

        [FormerlySerializedAs("sceneName")] [SerializeField]
        private string scenePath;

        /// <summary>
        /// The name of the scene.
        /// </summary>
        public string ScenePath => scenePath;

        // makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneField sceneField)
        {
            return sceneField?.ScenePath;
        }

        [Conditional("UNITY_EDITOR")]
        public void RefreshScenePath()
        {
#if UNITY_EDITOR
            scenePath = AssetDatabase.GetAssetPath(sceneAsset);
#endif
        }

        public SceneField(string scenePath)
        {
            this.scenePath = scenePath;
        }
    }
}