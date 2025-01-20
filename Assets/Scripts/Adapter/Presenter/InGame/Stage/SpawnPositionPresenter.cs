using Adapter.IView.InGame.Stage;
using Domain.IPresenter.InGame.Stage;
using UnityEngine;

namespace Adapter.Presenter.InGame.Stage
{
    public class SpawnPositionPresenter: ISpawnPositionPresenter
    {
        public SpawnPositionPresenter
        (
            IPositionView positionView
        )
        {
            PositionView = positionView;
        }
        
        public Vector3 SpawnPosition => PositionView.Position.position;
        
        private IPositionView PositionView { get; }
    }
}