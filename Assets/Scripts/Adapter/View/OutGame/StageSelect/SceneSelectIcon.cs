using Adapter.IView.Util.UI;
using Module.SceneReference;
using UnityEngine;

namespace Adapter.View.OutGame.StageSelect
{
    public class SceneSelectIcon : MonoBehaviour, ISceneGettableView
    {
        [SerializeField] private SceneReference sceneReference;

        public string SceneName => sceneReference.SceneName;
    }
}