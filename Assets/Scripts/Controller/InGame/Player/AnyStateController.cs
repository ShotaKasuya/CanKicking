using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Model.Global;
using Interface.Model.InGame;
using Interface.View.InGame;
using R3;
using Structure.InGame.Player;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Player;

public class AnyStateController : IAsyncStartable
{
    public AnyStateController
    (
        IPlayerView playerView,
        IPlayerCommandReceiver playerCommandReceiver,
        ILazyPlayerView lazyPlayerView,
        ISpawnEffectView spawnEffectView,
        IEffectSpawnModel effectSpawnModel,
        IKickPositionModel kickPositionModel,
        IBlockingOperationModel blockingOperationModel,
        CompositeDisposable compositeDisposable
    )
    {
        PlayerView = playerView;
        CommandReceiver = playerCommandReceiver;
        LazyPlayerView = lazyPlayerView;
        SpawnEffectView = spawnEffectView;
        EffectSpawnModel = effectSpawnModel;
        KickPositionModel = kickPositionModel;
        BlockingOperationModel = blockingOperationModel;
        CompositeDisposable = compositeDisposable;

        CommandResolver = new[]
        {
            UndoResolver
        };
    }

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        LazyPlayerView.PlayerView.Init(PlayerView);
        PlayerView.CollisionEnterEvent
            .SubscribeAwait(
                this,
                async (collision2D, controller, arg3) => await controller.SpawnEffect(collision2D, arg3),
                awaitOperation: AwaitOperation.Parallel
            )
            .AddTo(CompositeDisposable);
        CommandReceiver.Stream.Subscribe(
                this,
                (command, controller) => controller.CommandTrampoline(command)
            )
            .AddTo(CompositeDisposable);
        await InitEffect(cancellation);
    }

    private const string InitEffectContext = "Initialize Effect Context";

    private async UniTask InitEffect(CancellationToken token)
    {
        using var handle = BlockingOperationModel.SpawnOperation(InitEffectContext);

        await SpawnEffectView.Initialize(token);
    }

    /// <summary>
    /// 衝突に反応し、エフェクトを生成する
    /// </summary>
    private UniTask SpawnEffect(Collision2D collision, CancellationToken cancellationToken)
    {
        var effectThreshold = EffectSpawnModel.SpawnThreshold;
        var sqrThreshold = effectThreshold * effectThreshold;
        var velocitySqrMagnitude = collision.relativeVelocity.sqrMagnitude;
        var lastTask = UniTask.CompletedTask;

        if (velocitySqrMagnitude < sqrThreshold) return lastTask;

        var effectLength = EffectSpawnModel.EffectLength;
        for (var i = 0; i < collision.contactCount; i++)
        {
            var contact = collision.contacts[i];
            lastTask = SpawnEffectView.SpawnEffect(contact.point, contact.normal, effectLength, cancellationToken);
        }

        return lastTask;
    }

    private void CommandTrampoline(PlayerInteractCommand command)
    {
        CommandResolver[(int)command.Type](this, command);
    }

    private static Unit UndoResolver(AnyStateController self, PlayerInteractCommand command)
    {
        var prevPosition = self.KickPositionModel.PopPosition().Unwrap();
        self.PlayerView.ResetPosition(prevPosition);

        return Unit.Default;
    }

    private CompositeDisposable CompositeDisposable { get; }
    private Func<AnyStateController, PlayerInteractCommand, Unit>[] CommandResolver { get; }
    private IPlayerCommandReceiver CommandReceiver { get; }
    private IPlayerView PlayerView { get; }
    private ILazyPlayerView LazyPlayerView { get; }
    private ISpawnEffectView SpawnEffectView { get; }
    private IEffectSpawnModel EffectSpawnModel { get; }
    private IKickPositionModel KickPositionModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}