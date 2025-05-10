using System;
using UnityEngine;

namespace Structure.InGame.Player
{
    /// <summary>
    /// プレイヤーのベースとなる初期ステータスを定義する
    /// </summary>
    [Serializable]
    public class PlayerInitialStatus
    {
        public float KickPower => kickPower;
        public float KickableUpperLimit => kickableUpperLimit;

        [SerializeField] private float kickPower;
        [SerializeField] private float kickableUpperLimit;
    }

    [Serializable]
    public struct KickPower
    {
        public float BasePower => basePower;

        [SerializeField] private float basePower;
    }
}