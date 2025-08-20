using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Module.FadeContainer.Runtime
{
    [Serializable]
    public class FadeContainer
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private FadeEntity[] fadeTargets;

        public IEnumerable<(Type, Transform)> Targets => fadeTargets
            .Select(x => (x.GetTargetType(), x.Target))
            .Where(x => x.Item1 is not null);

        public async UniTask FadeIn()
        {
            var task = UniTask.CompletedTask;
            for (int i = 0; i < fadeTargets.Length; i++)
            {
                var target = fadeTargets[i];
                task = target.Target.DOMove(target.FadeInPosition, fadeDuration).AsyncWaitForCompletion().AsUniTask();
            }

            await task;
        }

        public async UniTask FadeOut()
        {
            var task = UniTask.CompletedTask;
            for (int i = 0; i < fadeTargets.Length; i++)
            {
                var target = fadeTargets[i];
                task = target.Target.DOMove(target.FadeOutPosition, fadeDuration).AsyncWaitForCompletion().AsUniTask();
            }

            await task;
        }
    }
}