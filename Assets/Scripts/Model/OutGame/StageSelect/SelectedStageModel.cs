using Interface.OutGame.StageSelect;
using Module.SceneReference;

namespace Model.OutGame.StageSelect
{
    public class SelectedStageModel : ISelectedStageModel
    {
        public string SelectedStage { get; private set; }

        public void SetSelectedStage(string sceneReference)
        {
            SelectedStage = sceneReference;
        }
    }
}