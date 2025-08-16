using Interface.Global.Utility;
using Interface.InGame.Player;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Logic.InGame.Player;

public class CalcByScreenRatioLogic : ICalcKickPowerLogic
{
    public CalcByScreenRatioLogic
    (
        IScreenScaleModel screenScaleModel,
        IPullLimitModel pullLimitModel
    )
    {
        ScreenScaleModel = screenScaleModel;
        PullLimitModel = pullLimitModel;
    }

    public Vector2 CalcKickPower(Vector2 input)
    {
        var screen = ScreenScaleModel.Scale;
        var cancelLength = PullLimitModel.CancelRatio;
        var maxLength = PullLimitModel.MaxRatio;

        var result = InnerCalcKickPower(input, screen, cancelLength, maxLength);

        return result;
    }

    [BurstCompile]
    private static float2 InnerCalcKickPower(float2 input, float2 screenScale, float cancelLength, float maxLength)
    {
        var normalizedInput = math.normalize(input);
        var inputLength = math.length(input);
        var shorter = math.min(screenScale.x, screenScale.y);

        var inputRatio = inputLength / shorter;
        var clampedInputRatio = math.clamp(inputRatio, 0f, 1f);

        if (clampedInputRatio == 0f)
        {
            return float2.zero;
        }

        var result = (clampedInputRatio - cancelLength) / (maxLength - cancelLength); // InverseLerp
        result = math.clamp(result, 0, 1);

        return normalizedInput * result;
    }


    private IScreenScaleModel ScreenScaleModel { get; }
    private IPullLimitModel PullLimitModel { get; }
}