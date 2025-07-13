using System;
using Module.SceneReference;

namespace Interface.Global.Scene
{
    public interface IDependencyModel
    {
        public void PushDependencyScenes(ReadOnlySpan<SceneContext> dependency);
    }
}