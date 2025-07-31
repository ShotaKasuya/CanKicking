using Interface.OutGame.StageSelect;
using Module.SceneReference.AeLa.Utilities;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class SceneGettableView:MonoBehaviour, ISceneGettableView
    {
        public string Scene => sceneField;

        [SerializeField] private SceneField sceneField;
    }
}