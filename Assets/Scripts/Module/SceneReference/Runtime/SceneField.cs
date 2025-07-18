

namespace Module.SceneReference
{
// Source: http://answers.unity.com/comments/1374414/view.html

using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace AeLa.Utilities
{
	/// <summary>
	/// Unity editor-friendly scene reference field.
	/// </summary>
	[System.Serializable]
	public class SceneField
	{
		[SerializeField] private Object sceneAsset;
		[FormerlySerializedAs("sceneName")] [SerializeField] private string scenePath = "";

		/// <summary>
		/// The name of the scene.
		/// </summary>
		public string ScenePath => scenePath;

		// makes it work with the existing Unity methods (LoadLevel/LoadScene)
		public static implicit operator string(SceneField sceneField)
		{
			return sceneField?.ScenePath;
		}

		[Conditional("UNITY_EDITOR")]
		public void RefreshScenePath()
		{
#if UNITY_EDITOR
			scenePath = AssetDatabase.GetAssetPath(sceneAsset);
#endif
		}
	}

#if UNITY_EDITOR
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
				if (!EditorBuildSettings.scenes.Any(s => s.path == path))
				{
					style.normal.textColor = Color.red;
				}
				EditorGUI.LabelField(position, null, scenePath.stringValue, style);
				EditorGUI.EndDisabledGroup();
			}
			EditorGUI.EndProperty();
		}
	}
#endif
}}