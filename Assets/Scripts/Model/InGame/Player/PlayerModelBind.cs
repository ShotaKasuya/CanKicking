using ModuleExtension.VContainer;
using Structure.Utility;
using UnityEngine;
using VContainer;

namespace Model.InGame.Player
{
    [CreateAssetMenu(fileName = "PlayerModelBind", menuName = MenuName)]
    public class PlayerModelBind : ScriptableObject, IRegisterable
    {
        private const string MenuName = Constants.BindScriptableObject + "/" + nameof(PlayerModelBind);

        [SerializeField] private GroundDetectionModel groundDetectionModel;
        [SerializeField] private KickBasePowerModel kickBasePowerModel;
        [SerializeField] private PullLimitModel pullLimitModel;
        [SerializeField] private EffectSpawnModel effectSpawnModel;
        [SerializeField] private SoundEffectModel soundEffectModel;

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(groundDetectionModel).AsImplementedInterfaces();
            builder.RegisterInstance(kickBasePowerModel).AsImplementedInterfaces();
            builder.RegisterInstance(pullLimitModel).AsImplementedInterfaces();
            builder.RegisterInstance(effectSpawnModel).AsImplementedInterfaces();
            builder.RegisterInstance(soundEffectModel).AsImplementedInterfaces();
        }
    }
}