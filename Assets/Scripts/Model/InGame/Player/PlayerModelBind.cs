using Model.Global;
using Structure.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Model.InGame.Player
{
    [CreateAssetMenu(fileName = "PlayerModelBind", menuName = MenuName)]
    public class PlayerModelBind : ScriptableObject, IRegisterable
    {
        private const string MenuName = Constants.BindScriptableObject + "/" + nameof(PlayerModelBind);

        [SerializeField] private GroundDetectionModel groundDetectionModel;
        [SerializeField] private KickBasePowerModel kickBasePowerModel;
        [SerializeField] private PullLimitModel pullLimitModel;

        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(groundDetectionModel).AsImplementedInterfaces();
                componentsBuilder.AddInstance(kickBasePowerModel).AsImplementedInterfaces();
                componentsBuilder.AddInstance(pullLimitModel).AsImplementedInterfaces();
            });
        }
    }
}