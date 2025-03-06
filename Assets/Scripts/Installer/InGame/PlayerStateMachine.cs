using System.Collections.Generic;
using DataUtil.InGame.Player;
using Module.StateMachine;

namespace Installer.InGame
{
    public class PlayerStateMachine : AbstractStateMachine<PlayerStateType>
    {
        public PlayerStateMachine(IStateEntity<PlayerStateType> stateEntity,
            List<IStateBehaviour<PlayerStateType>> behaviourEntities) : base(stateEntity, behaviourEntities)
        {
        }
    }
}