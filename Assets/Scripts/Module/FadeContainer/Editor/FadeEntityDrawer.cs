using Module.FadeContainer.Runtime;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

namespace Module.FadeContainer.Editor
{
    [CustomPropertyDrawer(typeof(FadeEntity))]
    public class FadeTargetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var registerTypeProp = property.FindPropertyRelative("targetType");
            var fadeTargetProp = property.FindPropertyRelative("fadeTarget");
            var fadeInPosProp = property.FindPropertyRelative("fadeInPosition");
            var fadeOutPosProp = property.FindPropertyRelative("fadeOutPosition");

            // 行レイアウト管理
            var lineHeight = EditorGUIUtility.singleLineHeight + 2;
            var rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            // fadeTarget
            EditorGUI.PropertyField(rect, fadeTargetProp);
            rect.y += lineHeight;

            // コンポーネント選択 (Target が設定されているときだけ)
            if (fadeTargetProp.objectReferenceValue is Transform tr)
            {
                var comps = tr.GetComponents<Component>();
                var compNames = comps.Select(c => c.GetType().FullName).ToArray();

                // 現在選択されている型
                int currentIndex = -1;
                if (!string.IsNullOrEmpty(registerTypeProp.stringValue))
                {
                    currentIndex = Array.FindIndex(compNames,
                        n => n == Type.GetType(registerTypeProp.stringValue)?.FullName);
                }

                int newIndex = EditorGUI.Popup(rect, "Register Type", currentIndex, compNames);
                if (newIndex >= 0 && newIndex < comps.Length)
                {
                    var type = comps[newIndex].GetType();
                    registerTypeProp.stringValue = type.AssemblyQualifiedName;
                }
            }
            else
            {
                EditorGUI.LabelField(rect, "Register Type", "No Target");
            }

            rect.y += lineHeight;

            // fadeInPosition
            EditorGUI.PropertyField(rect, fadeInPosProp);
            rect.y += lineHeight;

            // fadeOutPosition
            EditorGUI.PropertyField(rect, fadeOutPosProp);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight + 2) * 4;
        }
    }
}