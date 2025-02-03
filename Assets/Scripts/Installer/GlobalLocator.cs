using Adapter.Presenter.Scene;
using Adapter.Presenter.Util.Input;
using Detail.View.InGame.Input;
using Detail.View.Scene;
using Domain.IPresenter.Scene;
using Domain.IPresenter.Util.Input;
using Module.Installer;
using UnityEngine;

namespace Installer
{
    public class GlobalLocator : InstallerBase
    {
        public static GlobalLocator Instance
        {
            get
            {
                if (_instance is null)
                {
                    var instance = FindFirstObjectByType<GlobalLocator>();
                    if (instance is null)
                    {
                        Debug.LogError($"find object failed! : {nameof(GlobalLocator)}");
                    }

                    _instance = instance;
                }

                return _instance;
            }
        }

        private static GlobalLocator _instance;

        protected override void CustomConfigure()
        {
            if (_instance is null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
            // View
            var inputView = new InputView();
            RegisterEntryPoints(inputView);
            var sceneLoadView = new SceneLoadView();

            // Presenter
            var sceneLoadPresenter = new SceneLoadPresenter(sceneLoadView);
            var fingerEventPresenter = new FingerEventPresenter(inputView);
            RegisterEntryPoints(fingerEventPresenter);
            RegisterInstance<IScenePresenter, SceneLoadPresenter>(sceneLoadPresenter);
            RegisterInstance<IFingerTouchEventPresenter, FingerEventPresenter>(fingerEventPresenter);
            RegisterInstance<IFingerTouchingEventPresenter, FingerEventPresenter>(fingerEventPresenter);
            RegisterInstance<IFingerReleaseEventPresenter, FingerEventPresenter>(fingerEventPresenter);
        }

        private void OnApplicationQuit()
        {
            _instance.Dispose();
            _instance = null;
        }
    }
}