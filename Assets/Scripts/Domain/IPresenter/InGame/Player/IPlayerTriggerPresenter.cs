using System;
using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IPlayerTriggerPresenter
    {
        public Action<Collider2D> TriggerEnterEvent { get; set; } 
    }
}