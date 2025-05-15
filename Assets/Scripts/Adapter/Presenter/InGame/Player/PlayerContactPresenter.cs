using System;
using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerContactPresenter : IPlayerContactPresenter, IPlayerTriggerPresenter, IDisposable
    {
        public PlayerContactPresenter
        (
            IContactView contactView,
            ITriggerEnterView triggerEnterView
        )
        {
            ContactView = contactView;
            TriggerEnterView = triggerEnterView;

            ContactView.ContactEvent += InvokeCollisionEvent;
            TriggerEnterView.TriggerEnterEvent += InvokeTriggerEvent;
        }

        private void InvokeCollisionEvent(Collision2D collision2D)
        {
            OnCollision?.Invoke(collision2D);
        }

        private void InvokeTriggerEvent(Collider2D collision2D)
        {
            TriggerEnterEvent?.Invoke(collision2D);
        }

        public Action<Collision2D> OnCollision { get; set; }
        public Action<Collider2D> TriggerEnterEvent { get; set; }

        private IContactView ContactView { get; }
        private ITriggerEnterView TriggerEnterView { get; }

        public void Dispose()
        {
            ContactView.ContactEvent -= InvokeCollisionEvent;
        }
    }
}