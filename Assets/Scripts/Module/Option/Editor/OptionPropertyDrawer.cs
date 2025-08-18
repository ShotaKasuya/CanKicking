using Module.Option.Runtime;
using UnityEditor;
using UnityEngine;

namespace Module.Option.Editor
{
    [CustomPropertyDrawer(typeof(Option<>), true)]
    public class OptionPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var isSomeProp = property.FindPropertyRelative("isSome");
            var valueProp = property.FindPropertyRelative("value");

            // ラベル付きの折りたたみヘッダー
            position.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                // isSome チェックボックス
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(position, isSomeProp, new GUIContent("Has Value"));

                // value（isSome の場合のみ編集可）
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                using (new EditorGUI.DisabledScope(!isSomeProp.boolValue))
                {
                    EditorGUI.PropertyField(position, valueProp, new GUIContent("Value"), true);
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded)
            {
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; // isSome
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"), true) +
                          EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }
    }
}