using Module.EditorExtension.Runtime;
using UnityEditor;
using UnityEngine;

namespace Module.EditorExtension.Editor
{
    [CustomPropertyDrawer(typeof(AutoAssignAttribute))]
    public class AutoAssignDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.objectReferenceValue == null)
            {
                var component = (property.serializedObject.targetObject as Component);
                if (component != null)
                {
                    var type = fieldInfo.FieldType;
                    var found = component.GetComponent(type);
                    if (found != null)
                    {
                        property.objectReferenceValue = found;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                }
            }

            EditorGUI.PropertyField(position, property, label);

            EditorGUI.EndProperty();
        } 
    }
}