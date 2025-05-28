using VContainer;

namespace Adapter.View.InGame.Ui
{
    public static class UiExpression
    {
        public static void Register(this IContainerBuilder builder, IRegisterable registerable)
        {
            registerable.Register(builder);
        }
    }

    public interface IRegisterable
    {
        public void Register(IContainerBuilder builder);
    }
}