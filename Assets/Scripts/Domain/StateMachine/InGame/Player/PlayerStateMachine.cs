using System.Collections.Generic;
using DataUtil.InGame.Player;
using Module.Installer;
using Module.StateMachine;

namespace Domain.StateMachine.InGame.Player
{
    public class PlayerStateMachine : AbstractStateMachine<PlayerStateType>, ITickable
    {
        public PlayerStateMachine(
            IStateEntity<PlayerStateType> stateEntity,
            List<IStateBehaviour<PlayerStateType>> behaviourEntities
        ) : base(stateEntity, behaviourEntities)
        {
        }
    }
}