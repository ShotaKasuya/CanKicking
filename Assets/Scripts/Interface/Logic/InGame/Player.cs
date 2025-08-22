using UnityEngine;

namespace Interface.InGame.Player;

public interface ICalcKickPowerLogic
{
    public Vector2 CalcKickPower(Vector2 input);
}