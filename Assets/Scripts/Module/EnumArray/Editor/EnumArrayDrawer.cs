using Module.EnumArray.Runtime;
using UnityEditor;
using UnityEngine;

namespace Module.EnumArray.Editor
{
    [CustomPropertyDrawer(typeof(EnumArrayAttribute))]
    public class EnumListDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enumListAttribute = (EnumArrayAttribute)attribute;
            var enumType = enumListAttribute.EnumType;
            var arrayProperty = property.FindPropertyRelative("array");

            var labelStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                normal = { textColor = Color.cyan }
            };
            EditorGUI.LabelField(
                new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                property.name, labelStyle);
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            if (!enumType.IsEnum)
            {
                EditorGUI.LabelField(position, label.text, "Use EnumList with Enum type.");
                return;
            }

            if (!arrayProperty.isArray)
            {
                EditorGUI.LabelField(position, label.text, "Property is not an array.");
                return;
            }

            var enumNames = System.Enum.GetNames(enumType);
            arrayProperty.arraySize = enumNames.Length;

            EditorGUI.BeginProperty(position, label, arrayProperty);

            for (int i = 0; i < enumNames.Length; i++)
            {
                var listElement = arrayProperty.GetArrayElementAtIndex(i);
                var enumName = enumNames[i];

                Rect elementRect = new Rect(
                    position.x,
                    position.y + i * EditorGUIUtility.singleLineHeight,
                    position.width,
                    EditorGUIUtility.singleLineHeight
                );

                EditorGUI.PropertyField(elementRect, listElement, new GUIContent(enumName), true);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var enumListAttribute = (EnumArrayAttribute)attribute;
            var enumType = enumListAttribute.EnumType;
            var arrayProperty = property.FindPropertyRelative("array");

            if (enumType.IsEnum && arrayProperty.isArray)
            {
                var enumCount = System.Enum.GetNames(enumType).Length + 2;
                return EditorGUIUtility.singleLineHeight * enumCount;
            }

            return EditorGUIUtility.singleLineHeight;
        }
    }
}