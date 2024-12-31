using System;
using UnityEngine;

namespace DataUtil.InGame.Player
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
}