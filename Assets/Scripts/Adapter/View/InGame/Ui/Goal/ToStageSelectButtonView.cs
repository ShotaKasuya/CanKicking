using Adapter.IView.InGame.Ui;
using Structure.Scene;

namespace Adapter.View.InGame.Ui.Goal
{
    public class ToStageSelectButtonView: SceneChangeButton, IGoalStateStageSelectButtonView
    {
        protected override void Invoke()
        {
            Subject.OnNext(SceneType.StageSelect);
        }
    }
}