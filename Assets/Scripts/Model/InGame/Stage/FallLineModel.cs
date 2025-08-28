using System;
using Interface.Model.InGame;
using UnityEngine;

namespace Model.InGame.Stage
{
    [Serializable]
    public class FallLineModel : IFallLineModel
    {
        [SerializeField] private float fallLine;

        public float FallLine => fallLine;
    }
}