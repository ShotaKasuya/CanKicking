namespace View.InGame.Input
{
    public partial class InGameInputView
    {
        public float Pool()
        {
            var input = Actions.Pinch;
            var pinchValue = input.ReadValue<float>();

            return pinchValue;
        }
    }
}