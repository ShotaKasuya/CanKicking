using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Global.Utility;
using Interface.InGame.Player;
using Interface.InGame.Primary;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Player;

public class AnyStateController : IInitializable
{
    public AnyStateController
    (
        IPlayerView playerView,
        ILazyPlayerView lazyPlayerView,
        ISpawnEffectView spawnEffectView,
        IEffectSpawnModel effectSpawnModel,
        IBlockingOperationModel blockingOperationModel,
        CompositeDisposable compositeDisposable
    )
    {
        PlayerView = playerView;
        LazyPlayerView = lazyPlayerView;
        SpawnEffectView = spawnEffectView;
        EffectSpawnModel = effectSpawnModel;
        BlockingOperationModel = blockingOperationModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Initialize()
    {
        LazyPlayerView.PlayerView.Init(PlayerView);
        PlayerView.CollisionEnterEvent
            .SubscribeAwait(
                this,
                async (collision2D, controller, arg3) => await controller.SpawnEffect(collision2D, arg3),
                awaitOperation: AwaitOperation.Parallel
            )
            .AddTo(CompositeDisposable);
        InitEffect().Forget();
    }

    private const string InitEffectContext = "Initialize Effect Context";

    private async UniTask InitEffect()
    {
        using var handle = BlockingOperationModel.SpawnOperation(InitEffectContext);

        await SpawnEffectView.Initialize();
    }

    private UniTask SpawnEffect(Collision2D collision, CancellationToken cancellationToken)
    {
        var effectThreshold = EffectSpawnModel.SpawnThreshold;
        var sqrThreshold = effectThreshold * effectThreshold;
        var velocitySqrMagnitude = collision.relativeVelocity.sqrMagnitude;
        var lastTask = UniTask.CompletedTask;

        if (velocitySqrMagnitude < sqrThreshold) return lastTask;

        var effectLength = EffectSpawnModel.EffectLength;
        for (int i = 0; i < collision.contactCount; i++)
        {
            var contact = collision.contacts[i];
            lastTask = SpawnEffectView.SpawnEffect(contact.point, contact.normal, effectLength, cancellationToken);
        }

        return lastTask;
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IPlayerView PlayerView { get; }
    private ILazyPlayerView LazyPlayerView { get; }
    private ISpawnEffectView SpawnEffectView { get; }
    private IEffectSpawnModel EffectSpawnModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}