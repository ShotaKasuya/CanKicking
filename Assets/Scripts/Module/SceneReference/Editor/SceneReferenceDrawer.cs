using System;
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

            // 高さ計算
            float lineHeight = EditorGUIUtility.singleLineHeight;
            float padding = EditorGUIUtility.standardVerticalSpacing;
            Rect typeRect = new(position.x, position.y, position.width, lineHeight);
            Rect sceneRect = new(position.x, position.y + lineHeight + padding, position.width, lineHeight);

            // sceneType の描画
            var sceneTypeProp = property.FindPropertyRelative("sceneType");
            EditorGUI.PropertyField(typeRect, sceneTypeProp);

            // 各プロパティ
            var sceneAssetProp = property.FindPropertyRelative("sceneAsset");
            var sceneNameProp = property.FindPropertyRelative("sceneName");

            // sceneType に応じた表示
            var sceneType = (SceneType)sceneTypeProp.enumValueIndex;
            var sceneLabel=String.Empty;

            switch (sceneType)
            {
                case SceneType.Local:
                    sceneLabel = "Local Scene";
                    break;
                case SceneType.Addressables:
                    sceneLabel = "Addressable Asset Scene";
                    break;
            }
            var newScene = EditorGUI.ObjectField(sceneRect, sceneLabel, sceneAssetProp.objectReferenceValue,
                typeof(SceneAsset), false);
            sceneNameProp.stringValue = newScene != null ? newScene.name : "";

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
        }
    }
#endif
}