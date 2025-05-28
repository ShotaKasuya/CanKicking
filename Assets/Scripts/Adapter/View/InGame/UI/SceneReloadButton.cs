using System;
using Structure.Scene;
using UnityEngine.SceneManagement;

namespace Adapter.View.InGame.UI
{
    public abstract class SceneReloadButton: SceneChangeButton
    {
        protected override void Invoke()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            Subject.OnNext((SceneType)Enum.Parse(typeof(SceneType), currentScene));
        }
    }
}