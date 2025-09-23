using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Module.FadeContainer.Runtime
{
    [Serializable]
    public class FadeContainer
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private FadeEntity[] fadeTargets;

        public UniTask FadeIn(CancellationToken token)
        {
            var task = UniTask.CompletedTask;
            for (int i = 0; i < fadeTargets.Length; i++)
            {
                var target = fadeTargets[i];
                task = LMotion.Create(target.FadeOutPosition, target.FadeInPosition, fadeDuration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                    .BindToPosition(target.Target)
                    .ToUniTask(cancellationToken: token);
            }

            return task;
        }

        public UniTask FadeOut(CancellationToken token)
        {
            var task = UniTask.CompletedTask;
            for (int i = 0; i < fadeTargets.Length; i++)
            {
                var target = fadeTargets[i];
                task = LMotion.Create(target.FadeInPosition, target.FadeOutPosition, fadeDuration)
                    .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                    .BindToPosition(target.Target)
                    .ToUniTask(cancellationToken: token);
            }

            return task;
        }
    }
}