using System.ComponentModel;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace Structure.Utility.Abstraction
{
    /// <summary>
    /// 参考
    /// https://nekojara.city/unity-input-system-pinch-and-multi-swipe#Composite%20Binding%E3%81%A7%E5%AE%9F%E8%A3%85%E3%81%99%E3%82%8B
    /// </summary>
    [DisplayName("Pinch Composite")]
    public class PinchComposite : InputBindingComposite<float>
    {
        [InputControl(layout = "Touch")] public int touch_zero;
        [InputControl(layout = "Touch")] public int touch_one;

        private static readonly TouchDeltaMagnitudeComparer Comparer = new TouchDeltaMagnitudeComparer();

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        private static void Initialize()
        {
            if (Touchscreen.current == null || Touchscreen.current.touches.Count < 2)
            {
                Debug.LogWarning("Touchscreen not available or insufficient touches");
            }

            // 登録する必要がある
            InputSystem.RegisterBindingComposite(typeof(PinchComposite), "PinchComposite");
        }

        /// <summary>
        /// 値の大きさを返す
        /// </summary>
        public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
        {
            return ReadValue(ref context);
        }

        /// <summary>
        /// ピンチ操作量の取得
        /// </summary>
        public override float ReadValue(ref InputBindingCompositeContext context)
        {
            var touchState0 = context.ReadValue<TouchState, TouchDeltaMagnitudeComparer>(touch_zero, Comparer);
            var touchState1 = context.ReadValue<TouchState, TouchDeltaMagnitudeComparer>(touch_one, Comparer);

            if (!touchState0.isInProgress || !touchState1.isInProgress) return 0;

            var pos0 = touchState0.position;
            var pos1 = touchState1.position;

            var delta0 = touchState0.delta;
            var delta1 = touchState1.delta;

            var result = CalcPinch(pos0, pos1, delta0, delta1);

            return result;
        }

        [BurstCompile]
        private static float CalcPinch(float2 pos0, float2 pos1, float2 delta0, float2 delta1)
        {
            var prevPos0 = pos0 - delta0;
            var prevPos1 = pos1 - delta1;

            var pinch = math.distance(pos0, pos1) - math.distance(prevPos0, prevPos1);

            return pinch;
        }
    }
}