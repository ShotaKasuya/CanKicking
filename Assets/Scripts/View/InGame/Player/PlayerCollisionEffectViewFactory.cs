using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.InGame.Player;
using Structure.Utility;
using UnityEngine;
using UnityEngine.VFX;

namespace View.InGame.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerCollisionEffectViewFactory), menuName = MenuName, order = 0)]
    public class PlayerCollisionEffectViewFactory : ScriptableObject, ISpawnEffectView
    {
        private const string MenuName =
            Constants.ViewFactoryScriptableObject + "/" + nameof(PlayerCollisionEffectViewFactory);

        [SerializeField] private int poolSize = 8;
        [SerializeField] private VisualEffect collisionEffectView;

        private AsyncObjectPool<VisualEffect> _collisionEffectPool;

        public async UniTask Initialize()
        {
            // スパイクが減りそう?
            await UniTask.SwitchToThreadPool();
            _collisionEffectPool = new AsyncObjectPool<VisualEffect>(collisionEffectView, poolSize);
            await UniTask.SwitchToMainThread();

            await _collisionEffectPool.InitializeAsync();
        }

        public async UniTask SpawnEffect(Vector2 spawnPoint, Vector2 angle, float duration, CancellationToken cancellationToken)
        {
            var effect = _collisionEffectPool.Get();
            effect.transform.position = spawnPoint;
            effect.transform.rotation = Quaternion.FromToRotation(Vector2.up, angle);
            effect.Play();
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: cancellationToken);
            }
            finally
            {
                _collisionEffectPool.Return(effect);
            }
        }
    }
}