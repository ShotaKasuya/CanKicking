using VContainer;

namespace Structure.Utility
{
    public interface IRegisterable
    {
        public void Register(IContainerBuilder builder);
    }
}