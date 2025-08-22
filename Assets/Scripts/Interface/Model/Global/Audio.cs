using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Interface.Global.Audio;

public interface IBgmModel
{
    public AssetReferenceT<AudioSource> Bgm { get; }
}
