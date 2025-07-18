using System;
using System.Collections.Generic;
using System.Linq;
using Interface.Global.Scene;
using Module.SceneReference;
using Module.SceneReference.AeLa.Utilities;
using UnityEngine;

namespace Model.Global.Scene
{
    [Serializable]
    public class SceneResourcesModel: ISceneResourcesModel
    {
        [SerializeField] private List<SceneField> resourceScenes;

        private List<SceneReleaseContext> _releaseContexts = new List<SceneReleaseContext>();
        
        public IReadOnlyList<string> GetSceneResources()
        {
            return resourceScenes.Select(x => (string)x).ToArray();
        }

        public void PushReleaseContext(SceneReleaseContext sceneReleaseContext)
        {
            _releaseContexts.Add(sceneReleaseContext);
        }

        public IReadOnlyList<SceneReleaseContext> GetSceneReleaseContexts()
        {
            return _releaseContexts;
        }
    }
}