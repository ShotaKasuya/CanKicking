using System;
using UnityEngine;

namespace DataUtil.InGame.Player
{
    /// <summary>
    /// キックのパワーがランダムに上下する際の定数データ
    /// </summary>
    [Serializable]
    public class KickRandomConfig
    {
        public float Speed => speed;
        
        [SerializeField] private float speed;
    }
}