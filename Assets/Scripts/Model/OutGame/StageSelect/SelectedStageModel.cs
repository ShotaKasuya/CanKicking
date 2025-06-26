using Interface.OutGame.StageSelect;
using Module.SceneReference;

namespace Model.OutGame.StageSelect
{
    public class SelectedStageModel : ISelectedStageModel
    {
        public SceneReference SelectedStage { get; private set; }

        public void SetSelectedStage(SceneReference sceneReference)
        {
            SelectedStage = sceneReference;
        }
    }
}