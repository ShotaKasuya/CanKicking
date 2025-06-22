using System;
using System.Collections.Generic;
using Adapter.IView.InGame.Ui;
using Adapter.IView.Scene;
using Cysharp.Threading.Tasks;
using Domain.IRepository.InGame;
using Domain.IRepository.InGame.Player;
using Domain.IRepository.Util;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;
using Structure.Scene;
using UnityEngine;
using VContainer.Unity;

namespace Domain.Controller.InGame
{
    /// <summary>
    /// 一時停止状態のUIを管理する
    /// </summary>
    public class StopStateController : UserInterfaceBehaviourBase, IStartable, IDisposable
    {
        public StopStateController
        (
            IMutPlayerStateRepository playerStateRepository,
            IPlayButtonView playButtonView,
            IReadOnlyList<IStopUiView> stopUiView,
            IStopStateStageSelectButtonView stageSelectButtonView,
            IStopStateReStartButtonView reStartButtonView,
            IStopStateScreenScaleSliderView screenScaleSliderView,
            IScreenWidthRepository screenWidthRepository,
            ISceneLoadView sceneLoadView,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Stop, stateEntity)
        {
            PlayerStateRepository = playerStateRepository;
            PlayButtonView = playButtonView;
            StageSelectButtonView = stageSelectButtonView;
            ReStartButtonView = reStartButtonView;
            StopUiView = stopUiView;
            ScreenScaleSliderView = screenScaleSliderView;
            ScreenWidthRepository = screenWidthRepository;
            SceneLoadView = sceneLoadView;

            CompositeDisposable = new CompositeDisposable();
        }

        public void Start()
        {
            OnExit();
            PlayButtonView.Performed
                .Where(_ => IsInState())
                .Subscribe(_ => Play())
                .AddTo(CompositeDisposable);
            StageSelectButtonView.Performed
                .Where(_ => IsInState())
                .Subscribe(Load)
                .AddTo(CompositeDisposable);
            ReStartButtonView.Performed
                .Where(_ => IsInState())
                .Subscribe(Load)
                .AddTo(CompositeDisposable);
            ScreenScaleSliderView.ChangeObservable
                .Where(_ => IsInState())
                .Subscribe(ScreenWidthRepository.SetWeight)
                .AddTo(CompositeDisposable);
        }

        public override void OnEnter()
        {
            Time.timeScale = ITimeScaleRepository.Stop;
            PlayerStateRepository.ChangeState(PlayerStateType.Stopping);
            foreach (var stopUiView in StopUiView)
            {
                stopUiView.Show().Forget();
            }
        }

        public override void OnExit()
        {
            Time.timeScale = ITimeScaleRepository.Normal;
            PlayerStateRepository.ChangeState(PlayerStateType.Idle);
            foreach (var stopUiView in StopUiView)
            {
                stopUiView.Hide().Forget();
            }
        }

        private void Play()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Normal);
        }

        private void Load(SceneType sceneType)
        {
            SceneLoadView.Load(sceneType);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IMutPlayerStateRepository PlayerStateRepository { get; }
        private IPlayButtonView PlayButtonView { get; }
        private IReadOnlyList<IStopUiView> StopUiView { get; }
        private IStopStateStageSelectButtonView StageSelectButtonView { get; }
        private IStopStateReStartButtonView ReStartButtonView { get; }
        private IStopStateScreenScaleSliderView ScreenScaleSliderView { get; }
        private IScreenWidthRepository ScreenWidthRepository { get; }
        private ISceneLoadView SceneLoadView { get; }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}