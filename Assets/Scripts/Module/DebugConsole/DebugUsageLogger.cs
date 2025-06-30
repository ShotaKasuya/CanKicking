using System.Linq;
using UnityEngine;

namespace Module.DebugConsole
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
    public class DebugUsageLogger : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var debugUsages = AssetDatabase.FindAssets("t:MonoScript")
                .Select(AssetDatabase.AssetPathToGUID)
                .Where(path => path.EndsWith(".cs"))
                .Select(path => new { path, text = System.IO.File.ReadAllText(path) })
                .Where(file => file.text.Contains("DebugConsole"))
                .ToList();

            if (debugUsages.Count > 0)
            {
                Debug.LogWarning("Debug Consoleの使用");
                foreach (var usage in debugUsages)
                {
                    Debug.LogWarning($"- 使用ファイル: {usage.path}");
                }
            }
        }
    }
#endif
}