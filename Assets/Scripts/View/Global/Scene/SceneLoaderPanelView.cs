using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface.Global.Scene;
using Module.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace View.Global.Scene
{
    public class SceneLoaderPanelView: MonoBehaviour, ISceneLoaderView
    {
        [SerializeField] private Image panel;
        [SerializeField] private float sequenceDuration;

        private const float Empty = 1f;
        private const float Filled = 1f;
        
        public async UniTask LoadScene(SceneReference sceneReference)
        {
            await panel.DOFillAmount(Filled, sequenceDuration)
                .AsyncWaitForCompletion();

            
            await panel.DOFillAmount(Empty, sequenceDuration)
                .AsyncWaitForCompletion();
        }
    }
}