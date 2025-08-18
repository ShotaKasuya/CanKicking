using System;
using Interface.OutGame.Title;
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