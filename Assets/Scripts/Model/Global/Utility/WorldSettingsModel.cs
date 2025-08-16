using Interface.Global.Audio;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Model.Global.Utility
{
    [CreateAssetMenu(fileName = "WorldSettings", menuName = "Scriptable/World Data", order = 1)]
    public class WorldSettingsModel: ScriptableObject, IBgmModel
    {
        public AssetReferenceT<AudioSource> Bgm => bgm;

        [SerializeField] private AssetReferenceT<AudioSource> bgm;
    }
}