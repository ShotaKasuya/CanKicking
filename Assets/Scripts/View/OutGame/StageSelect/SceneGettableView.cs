using Interface.OutGame.StageSelect;
using Module.SceneReference;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class SceneGettableView:MonoBehaviour, ISceneGettableView
    {
        public SceneReference Scene => sceneReference;

        [SerializeField] private SceneReference sceneReference;
    }
}