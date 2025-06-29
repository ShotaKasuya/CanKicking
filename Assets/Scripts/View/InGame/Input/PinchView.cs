using Module.DebugConsole;

namespace View.InGame.Input
{
    public partial class InGameInputView
    {
        public float Pool()
        {
            var input = Actions.Pinch;
            var pinchValue = input.ReadValue<float>();

            DebugTextView.Instance.SetText("pinch Value", pinchValue);

            return pinchValue;
        }
    }
}