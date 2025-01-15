namespace Adapter.IView.InGame.UI
{
    public interface IPowerView
    {
        public void SetPower(float power);
        public void Disable();
        public void Enable();
    }
}