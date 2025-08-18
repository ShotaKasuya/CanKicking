using System;
using System.Collections.Generic;
using System.Linq;
using Interface.Global.Scene;
using Module.SceneReference;
using Module.SceneReference.Runtime;
using UnityEngine;

namespace Model.Global.Scene
{
    [Serializable]
    public class SceneResourcesModel: IResourceScenesModel
    {
        [SerializeField] private List<SceneField> resourceScenes;

        private List<SceneContext> _releaseContexts = new List<SceneContext>();
        
        public IReadOnlyList<string> GetResourceScenes()
        {
            return resourceScenes.Select(x => (string)x).ToArray();
        }

        public void PushReleaseContext(SceneContext sceneContext)
        {
            _releaseContexts.Add(sceneContext);
        }

        public IReadOnlyList<SceneContext> GetSceneReleaseContexts()
        {
            return _releaseContexts;
        }
    }
}