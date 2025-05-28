using Adapter.IView.InGame.Ui;
using Structure.Scene;

namespace Adapter.View.InGame.Ui.Stop
{
    public class ToStageSelectButtonView: SceneChangeButton, IStopStateStageSelectButtonView
    {
        protected override void Invoke()
        {
            Subject.OnNext(SceneType.StageSelect);
        }
    }
}