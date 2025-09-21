using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Model.Global;
using Module.SceneReference.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class FirstSceneLoader : LifetimeScope
    {
        [SerializeField] private SceneField sceneField;

    }
}