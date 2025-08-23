using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public IReadOnlyList<FadeEntity> FadeTargets => fadeTargets;

        public IEnumerable<(Type, MonoBehaviour)> Targets => fadeTargets
            .Select(x => (x.GetTargetType(), x.targetObject))
            .Where(x => x.Item1 is not null);

        public UniTask FadeIn(CancellationToken token)
        {
            var task = UniTask.CompletedTask;
            for (int i = 0; i < fadeTargets.Length; i++)
            {
                var target = fadeTargets[i];
                task = target.Target.DOMove(target.FadeInPosition, fadeDuration)
                    .SetUpdate(true)
                    .AsyncWaitForCompletion().AsUniTask();
            }

            return task;
        }

        public UniTask FadeOut(CancellationToken token)
        {
            var task = UniTask.CompletedTask;
            for (int i = 0; i < fadeTargets.Length; i++)
            {
                var target = fadeTargets[i];
                task = target.Target.DOMove(target.FadeOutPosition, fadeDuration)
                    .SetUpdate(true)
                    .AsyncWaitForCompletion().AsUniTask();
            }

            return task;
        }
    }
}