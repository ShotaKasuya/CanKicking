using UnityEditor;
using UnityEngine;

namespace Module.EditorExtension.Editor.HierarchyMarker
{
    public class MarkerSettingsWindow : EditorWindow
    {
        private Color _markerColor;

        [MenuItem("Tools/Marker Settings")]
        public static void ShowWindow()
        {
            GetWindow<MarkerSettingsWindow>("Marker Settings");
        }

        private void OnEnable()
        {
            _markerColor = LoadColor();
        }

        private void OnGUI()
        {
            GUILayout.Label("Editor Marker Color", EditorStyles.boldLabel);
            _markerColor = EditorGUILayout.ColorField("Marker Color", _markerColor);

            if (GUILayout.Button("Apply"))
            {
                SaveColor(_markerColor);
                EditorApplication.RepaintHierarchyWindow();
            }

            if (GUILayout.Button("Reset to Default"))
            {
                _markerColor = new Color(1f, 0.9f, 0.4f, 0.3f); // デフォルト色
                SaveColor(_markerColor);
                EditorApplication.RepaintHierarchyWindow();
            }
        }

        private void SaveColor(Color color)
        {
            EditorPrefs.SetString(MarkerHighlighter.ColorPrefKey, ColorUtility.ToHtmlStringRGBA(color));
        }

        private Color LoadColor()
        {
            if (EditorPrefs.HasKey(MarkerHighlighter.ColorPrefKey))
            {
                string html = EditorPrefs.GetString(MarkerHighlighter.ColorPrefKey);
                if (ColorUtility.TryParseHtmlString("#" + html, out var color))
                    return color;
            }

            return new Color(1f, 0.9f, 0.4f, 0.3f); // デフォルト
        }
    }
}