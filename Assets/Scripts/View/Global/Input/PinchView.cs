namespace View.Global.Input
{
    public partial class GlobalInputView
    {
        public float Pool()
        {
            var input = Actions.Pinch;
            var pinchValue = input.ReadValue<float>();

            return pinchValue;
        }
    }
}