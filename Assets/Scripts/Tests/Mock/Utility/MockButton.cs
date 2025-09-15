using R3;

namespace Tests.Mock.Utility
{
    public abstract class AbstractMockEventer<T>
    {
        private readonly Subject<T> _subject = new();
        public Observable<T> Performed => _subject;
        public void SimulateClick(T value) => _subject.OnNext(value);
        public bool IsInteractable { get; private set; }
        
        
        public void EnableInteraction()
        {
            IsInteractable = true;
        }

        public void DisableInteraction()
        {
            IsInteractable = false;
        }
    }
}