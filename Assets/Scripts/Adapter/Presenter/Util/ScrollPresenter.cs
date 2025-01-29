using Adapter.IView.Util;
using DataUtil.Util.Installer;
using Domain.IPresenter.Util;
using UnityEngine;

namespace Adapter.Presenter.Util
{
    public class ScrollPresenter : IScrollPresenter, ITickable
    {
        public ScrollPresenter
        (
            IMovableView movableView
        )
        {
            _velocity = Vector2.zero;
            MovableView = movableView;
        }

        public void AddVelocity(Vector2 power)
        {
            _velocity += power;
        }

        public void Tick(float deltaTime)
        {
            if (_velocity.sqrMagnitude <= 0.1f)
            {
                return;
            }

            MovableView.Translate(_velocity);
            _velocity *= MovableView.Damping;
        }

        // FIXME これをrepositoryに置いたほうがいいかも?
        private Vector2 _velocity;

        private IMovableView MovableView { get; }
    }
}