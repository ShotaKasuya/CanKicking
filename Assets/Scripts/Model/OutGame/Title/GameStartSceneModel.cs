using System;
using Interface.Model.OutGame;
using Module.SceneReference.Runtime;
using UnityEngine;

namespace Model.OutGame.Title
{
    [Serializable]
    public class GameStartSceneModel : IStartSceneModel
    {
        [SerializeField] private SceneField sceneField;

        public string GetStartSceneName()
        {
            return sceneField;
        }
    }
}