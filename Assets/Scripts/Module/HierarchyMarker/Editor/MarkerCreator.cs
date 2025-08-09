using UnityEditor;
using UnityEngine;

namespace Module.HierarchyMarker.Editor
{
    public static class MarkerCreator
    {
        private const int TargetLength = 25;
        public const string MarkerTag = "EditorMarker";

        [MenuItem("GameObject/Create Empty Marker", false, 0)]
        private static void CreateEmptyMarker(MenuCommand menuCommand)
        {
            var marker = new GameObject()
            {
                name = DecorateName("Marker"),
                tag = MarkerTag,
            };

            Undo.RegisterCreatedObjectUndo(marker, "Create Empty Marker");
            GameObjectUtility.SetParentAndAlign(marker, menuCommand.context as GameObject);
            Selection.activeObject = marker;
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyChanged()
        {
            var allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach (var gameObject in allObjects)
            {
                if (gameObject.CompareTag(MarkerTag) &&
                    gameObject.name != DecorateName(ExtractRawName(gameObject.name)))
                {
                    gameObject.name = DecorateName(ExtractRawName(gameObject.name));
                }
            }
        }

        private static string DecorateName(string rawName)
        {
            var totalPadding = Mathf.Max(0, TargetLength - rawName.Length);
            var sidePadding = totalPadding / 2;
            return new string('=', sidePadding) + rawName + new string('=', totalPadding - sidePadding);
        }

        private static string ExtractRawName(string decorated)
        {
            return decorated.Trim('=');
        }
    }
}