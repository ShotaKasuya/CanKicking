using Interface.OutGame.StageSelect;
using Structure.Utility;
using TNRD;
using UnityEngine;
using VContainer;
using VContainer.ModuleExtension;
using VContainer.Unity;

namespace Model.OutGame.StageSelect
{
    [CreateAssetMenu(fileName = "Stages", menuName = MenuName)]
    public class StagesBind: ScriptableObject, IRegisterable
    {
        private const string MenuName = Constants.BindScriptableObject + "/" + nameof(StagesBind);

        [SerializeField] private SerializableInterface<IStageScenesModel> stageScenes;

        public void Register(IContainerBuilder builder)
        {
           builder.UseComponents(componentsBuilder =>
           {
               componentsBuilder.AddInstance(stageScenes.Value);
           });
        }
    }
}