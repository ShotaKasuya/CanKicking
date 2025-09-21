using System;
using Interface.Model.OutGame;
using Module.SceneReference.Runtime;
using UnityEngine;

namespace Model.OutGame
{
    [Serializable]
    public class EntrySceneModel : IEntrySceneModel
    {
        [SerializeField] private SceneField titleScene;
        [SerializeField] private SceneField tutorialScene;
        
        public string TitleScene => titleScene;
        public string TutorialScene => tutorialScene;
    }
}