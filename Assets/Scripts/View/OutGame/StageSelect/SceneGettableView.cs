using Interface.View.OutGame;
using Module.SceneReference.Runtime;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class SceneGettableView : MonoBehaviour, ISceneGettableView
    {
        public string Scene => sceneField;

        [SerializeField] private SceneField sceneField;
    }
}