using System;
using Interface.InGame.Player;
using Structure.Utility;
using UnityEngine;

namespace Model.InGame.Player;

[Serializable]
public class GroundDetectionModel : IGroundDetectionModel
{
    public RayCastInfo GroundDetectionInfo => new RayCastInfo(Vector2.down, distance, layerMask);
    public float MaxSlope => maxSlope;

    [SerializeField] private float maxSlope;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;
}

[Serializable]
public class KickBasePowerModel : IKickBasePowerModel
{
    public float BasePower => basePower;

    [SerializeField] private float basePower;
}

[Serializable]
public class PullLimitModel : IPullLimitModel
{
    public float CancelRatio => cancelRatio;
    public float MaxRatio => limitRatio;

    [SerializeField, Range(0.01f, 1.0f)] private float limitRatio = 0.7f;
    [SerializeField, Range(0.01f, 1.0f)] private float cancelRatio = 0.1f;
}