using Adapter.IView.InGame.UI;
using Structure.Scene;

namespace Adapter.View.InGame.UI
{
    public class ToStageSelectButtonView: SceneChangeButton, IStopStateStageSelectButtonView
    {
        protected override void Invoke()
        {
            Subject.OnNext(SceneType.StageSelect);
        }
    }
}