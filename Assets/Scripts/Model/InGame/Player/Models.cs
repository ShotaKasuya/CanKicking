using System;
using Interface.Model.InGame;
using Structure.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.InGame.Player
{
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
        public float KickPower => kickPower;
        public float RotationPower => rotationPower;

        [SerializeField] private float kickPower;
        [SerializeField] private float rotationPower;
    }

    [Serializable]
    public class PullLimitModel : IPullLimitModel
    {
        public float CancelRatio => cancelRatio;
        public float MaxRatio => limitRatio;

        [SerializeField, Range(0.01f, 1.0f)] private float limitRatio = 0.7f;
        [SerializeField, Range(0.01f, 1.0f)] private float cancelRatio = 0.1f;
    }

    [Serializable]
    public class EffectSpawnModel : IEffectSpawnModel
    {
        public float SpawnThreshold => spawnThresholdVelocity;
        public float EffectLength => effectShowLength;

        [SerializeField] private float spawnThresholdVelocity;
        [SerializeField] private float effectShowLength;
    }

    [Serializable]
    public class SoundEffectModel : IPlayerSoundModel
    {
        [SerializeField] private AudioClip[] kickSounds;
        [SerializeField] private AudioClip[] boundSounds;

        public AudioClip GetKickSound()
        {
            var len = kickSounds.Length;
            var selection = Random.Range(0, len);
            return kickSounds[selection];
        }

        public AudioClip GetBoundSound()
        {
            var len = boundSounds.Length;
            var selection = Random.Range(0, len);
            return boundSounds[selection];
        }
    }
}