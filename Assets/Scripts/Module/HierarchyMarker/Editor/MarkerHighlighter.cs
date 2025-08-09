using UnityEditor;
using UnityEngine;

namespace Module.HierarchyMarker.Editor
{
    [InitializeOnLoad]
    public static class MarkerHighlighter
    {
        public static string ColorPrefKey { get; } = "ColorPrefKey";

        static MarkerHighlighter()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null && obj.CompareTag(MarkerCreator.MarkerTag))
            {
                var markerColor = LoadColor();
                EditorGUI.DrawRect(selectionRect, markerColor);
            }
        }
        
        private static Color LoadColor()
        {
            if (EditorPrefs.HasKey(ColorPrefKey))
            {
                string html = EditorPrefs.GetString(ColorPrefKey);
                if (ColorUtility.TryParseHtmlString("#" + html, out var color))
                    return color;
            }

            return new Color(1f, 0.9f, 0.4f, 0.3f); // fallback
        }
    }
}