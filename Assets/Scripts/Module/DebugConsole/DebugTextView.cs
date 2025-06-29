using System.Collections.Generic;
using UnityEngine;

namespace Module.DebugConsole
{
    public class DebugTextView : MonoBehaviour
    {
        private static DebugTextView _instance;

        public static DebugTextView Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("DebugConsole");
                    _instance = obj.AddComponent<DebugTextView>();
                    DontDestroyOnLoad(obj);
                }

                return _instance;
            }
        }

        private Dictionary<string, string> Dictionary { get; } = new Dictionary<string, string>();
        private Vector2 _scrollPosition = Vector2.zero;

        /// <summary>
        /// 表示するテキストを登録・更新する
        /// </summary>
        public void SetText<T>(string key, T value)
        {
            Dictionary[key] = value.ToString();
        }

        /// <summary>
        /// 表示から削除する
        /// </summary>
        public void RemoveText(string key)
        {
            if (Dictionary.ContainsKey(key))
            {
                Dictionary.Remove(key);
            }
        }

        /// <summary>
        /// 全ての表示をクリア
        /// </summary>
        public void Clear()
        {
            Dictionary.Clear();
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 30;
            style.normal.textColor = Color.red;

            GUILayout.BeginArea(new Rect(10, 10, 400, Screen.height - 20));
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            foreach (var line in Dictionary)
            {
                GUILayout.Label($"{line.Key}: {line.Value}", style);
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}