using System;
using Interface.OutGame.Title;
using Module.SceneReference;
using UnityEngine;

namespace Model.OutGame.Title
{
    [Serializable]
    public class GameStartSceneModel: IGameStartSceneModel
    {
        [SerializeField] private SceneReference sceneReference;
        public SceneReference GetStartScene()
        {
            return sceneReference;
        }
    }
}