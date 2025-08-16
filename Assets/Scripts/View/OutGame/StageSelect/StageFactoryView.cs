using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interface.OutGame.StageSelect;
using UnityEngine;
using UnityEngine.Splines;

namespace View.OutGame.StageSelect
{
    [RequireComponent(typeof(SplineContainer))]
    public class StageFactoryView : MonoBehaviour, IStageIconFactoryView
    {
        [SerializeField] private AbstractStageIconView stageIconView;
        private SplineContainer _splineContainer;

        private void Awake()
        {
            _splineContainer = GetComponent<SplineContainer>();
        }

        public async UniTask MakeIcons(IReadOnlyList<string> sceneNames)
        {
            var spline = _splineContainer.Spline;
            var count = sceneNames.Count;

            var handle = InstantiateAsync(stageIconView, count);
            var instances = await handle.ToUniTask();

            for (int i = 0; i < count; i++)
            {
                var scene = sceneNames[i];
                var instance = instances[i];

                var splineRatio = (float)i / Mathf.Max(1, count - 1);
                var position = spline.EvaluatePosition(splineRatio);

                instance.Initialize(scene, position);
            }
        }

        public abstract class AbstractStageIconView : MonoBehaviour, ISceneGettableView
        {
            public string Scene => _scene;

            private string _scene;

            public void Initialize(string sceneName, Vector3 position)
            {
                _scene = sceneName;
                transform.position = position;
                OnInit();
            }

            protected virtual void OnInit()
            {
            }
        }
    }
}