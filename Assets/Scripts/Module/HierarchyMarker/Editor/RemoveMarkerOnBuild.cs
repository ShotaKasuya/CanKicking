using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Module.HierarchyMarker.Editor
{
    public class RemoveMarkerOnBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            foreach (var gameObject in allObjects)
            {
                if (gameObject.CompareTag(MarkerCreator.MarkerTag))
                {
                    Object.DestroyImmediate(gameObject);
                }
            }
        }
    }
}