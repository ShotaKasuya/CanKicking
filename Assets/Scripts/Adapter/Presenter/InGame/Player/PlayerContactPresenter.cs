using System;
using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class PlayerContactPresenter: IPlayerContactPresenter, IDisposable
    {
        public PlayerContactPresenter
        (
            IContactView contactView
        )
        {
            ContactView = contactView;

            ContactView.ContactEvent += InvokeCollisionEvent;
        }

        private void InvokeCollisionEvent(Collision2D collision2D)
        {
            OnCollision?.Invoke(collision2D);
        }
            
        public Action<Collision2D> OnCollision { get; set; }
        
        private IContactView ContactView { get; }

        public void Dispose()
        {
            ContactView.ContactEvent -= InvokeCollisionEvent;
        }
    }
}