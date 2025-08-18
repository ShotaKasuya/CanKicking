using Interface.Global.Input;
using Interface.OutGame.StageSelect;
using Module.Option;
using Module.Option.Runtime;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace View.OutGame.StageSelect
{
    public class SelectionView : IStageSelectionView, IStartable
    {
        public SelectionView
        (
            ITouchView touchView,
            CompositeDisposable compositeDisposable
        )
        {
            TouchView = touchView;
            CompositeDisposable = compositeDisposable;
            SelectSubject = new Subject<Option<string>>();
        }

        public void Start()
        {
            _mainCamera = Camera.main!;
            TouchView.TouchEvent
                .Subscribe(this, (argument, view) => view.OnClick(argument))
                .AddTo(CompositeDisposable);
        }

        private void OnClick(TouchStartEventArgument touchStartEventArgument)
        {
            var screenPosition = touchStartEventArgument.TouchPosition;
            var worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

            var castHit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (castHit.collider is not null)
            {
                var collider = castHit.collider;
                if (collider.TryGetComponent<ISceneGettableView>(out var sceneGettableView))
                {
                    SelectSubject.OnNext(Option<string>.Some(sceneGettableView.Scene));
                    return;
                }
            }

            SelectSubject.OnNext(Option<string>.None());
        }

        public Observable<Option<string>> SelectEvent => SelectSubject;

        private CompositeDisposable CompositeDisposable { get; }
        private Camera _mainCamera;
        private Subject<Option<string>> SelectSubject { get; }
        private ITouchView TouchView { get; }
    }
}