using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Adapter.View.InGame.UI
{
    [Serializable]
    public class StopUi
    {
        [SerializeField] private PlayButtonView playButtonView;
        [SerializeField] private ToStageSelectButtonView stageSelectButtonView;
        [SerializeField] private ReStartButtonView reStartButtonView;

        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(playButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(stageSelectButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(reStartButtonView).AsImplementedInterfaces();
            });
        }
    }
}