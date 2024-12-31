using System;
using DataUtil.InGame.Player;

namespace Domain.IPresenter.InGame.Player
{
    public interface IKickPresenter
    {
        Action<ClickType> ClickEvent { get; set; }
    }
}