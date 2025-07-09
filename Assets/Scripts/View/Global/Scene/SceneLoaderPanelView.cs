using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface.Global.Scene;
using Module.SceneReference;
using ModuleExtension.SceneReferenceExtension;
using UnityEngine;
using UnityEngine.UI;

namespace View.Global.Scene
{
    public class SceneLoaderPanelView: MonoBehaviour, ISceneLoaderView
    {
        [SerializeField] private Image panel;
        [SerializeField] private float sequenceDuration;

        private const float Empty = 0f;
        private const float Filled = 1f;

        private void Awake()
        {
            panel.enabled = false;
            panel.fillAmount = Empty;
        }

        public async UniTask LoadScene(SceneReference sceneReference)
        {
            panel.enabled = true;
            await panel.DOFillAmount(Filled, sequenceDuration)
                .AsyncWaitForCompletion().AsUniTask();

            await sceneReference.LoadAsync();
            
            await panel.DOFillAmount(Empty, sequenceDuration)
                .AsyncWaitForCompletion().AsUniTask();
            panel.enabled = false;
        }
    }
}