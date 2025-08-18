using Interface.Global.Utility;
using Module.Option;
using Module.Option.Runtime;
using UnityEngine;

namespace Model.Global.SaveData
{
    /// <summary>
    /// ステージをクリアした際のジャンプ回数
    /// </summary>
    public class ClearRecordModel : IClearRecordModel
    {
        public void Save(string key, int jumpCount)
        {
            if (!PlayerPrefs.HasKey(key)) return;
            var saveData = PlayerPrefs.GetInt(key);
            if (jumpCount < saveData)
            {
                PlayerPrefs.SetInt(key, jumpCount);
            }
        }

        public Option<int> Load(string key)
        {
            if (!PlayerPrefs.HasKey(key)) return Option<int>.None();

            var jumpCount = PlayerPrefs.GetInt(key);
            return Option<int>.Some(jumpCount);
        }
    }
}