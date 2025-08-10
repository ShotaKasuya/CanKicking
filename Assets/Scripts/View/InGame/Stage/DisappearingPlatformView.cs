using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Module.TagSelector.Runtime;
using UnityEngine;
using UnityEngine.VFX;

namespace View.InGame.Stage
{
    /// <summary>
    /// 消える床
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class DisappearingPlatformView : MonoBehaviour
    {
        [SerializeField] private TagSelector playerTag;
        [SerializeField] private float disappearTime;
        [SerializeField] private float respawnTime;
        [SerializeField] private VisualEffect disappearingEffect;

        private State _currentState;
        private GameObject _selfObject;

        private void Awake()
        {
            _selfObject = gameObject;
        }

        private enum State
        {
            Idle,
            Disappearing,
            Hide,
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_currentState == State.Idle) return;
            if (!other.gameObject.CompareTag(playerTag)) return;

            DisappearRoutine(destroyCancellationToken).Forget();
        }

        private async UniTask DisappearRoutine(CancellationToken cancellationToken)
        {
            _currentState = State.Disappearing;
            disappearingEffect.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(disappearTime), cancellationToken: cancellationToken);

            _currentState = State.Hide;
            disappearingEffect.Stop();
            _selfObject.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(respawnTime), cancellationToken: cancellationToken);

            _currentState = State.Idle;
            _selfObject.SetActive(true);
        }
    }
}