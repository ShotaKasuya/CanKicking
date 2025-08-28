using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Interface.Model.Global;

public interface IBgmModel
{
    public AssetReferenceT<AudioSource> Bgm { get; }
}
