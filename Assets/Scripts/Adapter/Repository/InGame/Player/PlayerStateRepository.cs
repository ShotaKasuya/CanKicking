using System;
using Domain.IRepository.InGame.Player;
using Module.StateMachine;
using Structure.InGame.Player;
using UnityEngine;

namespace Adapter.Repository.InGame.Player
{
    public class PlayerStateRepository : AbstractStateType<PlayerStateType>, IMutPlayerStateRepository,
        IPlayerStateRepository
    {
        public PlayerStateRepository() : base(PlayerStateType.Idle)
        {
        }
    }

    [Serializable]
    public class RotationStateRepository : IRotationStateRepository
    {
        public float RotationAngle
        {
            get
            {
                if (_rotationState == RotationStateType.Positive)
                {
                    return rotationAngle;
                }
                else
                {
                    return -rotationAngle;
                }
            }
        }

        private RotationStateType _rotationState;
        [SerializeField] private float rotationAngle = 180f;

        public RotationStateType Read()
        {
            return _rotationState;
        }

        public void Toggle()
        {
            if (_rotationState == RotationStateType.Negative)
            {
                _rotationState = RotationStateType.Positive;
            }
            else if (_rotationState == RotationStateType.Positive)
            {
                _rotationState = RotationStateType.Negative;
            }
        }

        public void Set(RotationStateType type)
        {
            _rotationState = type;
        }
    }
}