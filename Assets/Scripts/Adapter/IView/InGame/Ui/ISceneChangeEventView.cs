using System;
using UnityEngine;

namespace Adapter.IView.InGame.UI
{
    public interface ISceneChangeEventView
    {
        public Action<string> SceneChangeEvent { get; set; }
    }
    
    public abstract class SceneChangeButtonViewBase: MonoBehaviour, ISceneChangeEventView
    {
        public Action<string> SceneChangeEvent { get; set; }
    }
}