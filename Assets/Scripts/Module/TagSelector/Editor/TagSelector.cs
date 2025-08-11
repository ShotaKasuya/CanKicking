using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Module.TagSelector.Editor
{
    [CustomPropertyDrawer(typeof(Runtime.TagSelector))]
    public class TagSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var tagProp = property.FindPropertyRelative("tag");
            if (tagProp == null)
            {
                EditorGUI.LabelField(position, label.text, "Missing 'tag' field");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            var tags = InternalEditorUtility.tags;
            var currentIndex = 0;
            var currentTag = tagProp.stringValue ?? string.Empty;

            var displayedTags = new string[tags.Length + 1];
            displayedTags[0] = "<None>";
            for (int i = 0; i < tags.Length; i++) displayedTags[i + 1] = tags[i];

            // 選択されている`tag`のインデックスを探す
            for (int i = 0; i < tags.Length; i++)
            {
                if (tags[i] == currentTag)
                {
                    currentIndex = i + 1;
                    break;
                }
            }

            var newIndex = EditorGUI.Popup(position, label.text, currentIndex, displayedTags);
            var newTag = (newIndex <= 0) ? string.Empty : displayedTags[newIndex];

            if (newTag != tagProp.stringValue)
            {
                tagProp.stringValue = newTag;
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.EndProperty();
        }
    }
}