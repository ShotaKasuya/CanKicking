using UnityEngine;

namespace Module.SceneReference.Editor
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var sceneAssetProp = property.FindPropertyRelative("sceneAsset");
            var sceneNameProp = property.FindPropertyRelative("sceneName");

            var newScene = EditorGUI.ObjectField(position, label, sceneAssetProp.objectReferenceValue,
                typeof(SceneAsset), false);
            sceneAssetProp.objectReferenceValue = newScene;

            if (newScene != null)
            {
                sceneNameProp.stringValue = newScene.name;
            }
            else
            {
                sceneNameProp.stringValue = "";
            }

            EditorGUI.EndProperty();
        }
    }

#endif
}