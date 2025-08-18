using System.Linq;
using Module.SceneReference.Runtime;
using UnityEditor;
using UnityEngine;

namespace Module.SceneReference.Editor
{
    /// <summary>
    /// Provides a <see cref="PropertyDrawer"/> for <see cref="SceneField"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label
        )
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            var sceneAsset = property.FindPropertyRelative("sceneAsset");
            var scenePath = property.FindPropertyRelative("scenePath");

            position = EditorGUI.PrefixLabel(
                totalPosition: position,
                id: GUIUtility.GetControlID(FocusType.Passive),
                label: label
            );

            if (sceneAsset != null)
            {
                // warn if scene isn't in build settings
                string path = AssetDatabase.GetAssetPath(
                    assetObject: sceneAsset.objectReferenceValue
                );

                EditorGUI.BeginChangeCheck();

                var value = sceneAsset.objectReferenceValue;
                value = EditorGUI.ObjectField(
                    position: position,
                    obj: value,
                    objType: typeof(SceneAsset),
                    allowSceneObjects: false
                );

                var valuePath = value ? AssetDatabase.GetAssetPath(value) : null;

                if (value && (EditorGUI.EndChangeCheck() || valuePath != scenePath.stringValue))
                {
                    sceneAsset.objectReferenceValue = value;
                    scenePath.stringValue = valuePath;
                }

                // name label
                EditorGUI.BeginDisabledGroup(true);
                var style = new GUIStyle(EditorStyles.label);
                if (EditorBuildSettings.scenes.All(s => s.path == path))
                {
                    style.normal.textColor = Color.red;
                }

                EditorGUI.LabelField(position, null, scenePath.stringValue, style);
                EditorGUI.EndDisabledGroup();
            }

            EditorGUI.EndProperty();
        }
    }
}