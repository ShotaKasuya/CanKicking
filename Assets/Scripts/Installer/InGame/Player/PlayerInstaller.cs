using Controller.InGame.Player;
using Model.InGame.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Input;
using View.InGame.Player;

namespace Installer.InGame.Player
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private AimView aimView;
        [SerializeField] private PlayerModelBind playerModelBind;

        protected override void Configure(IContainerBuilder builder)
        {
            var playerView = GetComponent<PlayerView>();
            
            // View
            builder.RegisterInstance(playerView).AsImplementedInterfaces();
            builder.RegisterComponent(aimView).AsImplementedInterfaces();
            builder.Register<InGameInputView>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Model
            playerModelBind.Register(builder);

            // Controller
            builder.Register<PlayerState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<IdleController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<AimingController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<FryingController>(Lifetime.Scoped).AsImplementedInterfaces();
        }

#if UNITY_EDITOR
        private PlayerState _state;

        private void Start()
        {
            _state = Container.Resolve<PlayerState>();
        }

        private void OnGUI()
        {
            var style = new GUIStyle()
            {
                fontSize = 130
            };
            GUI.Label(new Rect(10, 10, 100, 20), _state.State.ToString(), style);
        }
#endif
    }
}