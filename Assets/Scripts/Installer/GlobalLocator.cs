using System;
using System.Collections.Generic;
using Adapter.Presenter.Scene;
using Adapter.Presenter.Util.Input;
using DataUtil.Util.Installer;
using Detail.View.InGame.Input;
using Detail.View.Scene;
using Domain.IPresenter.Scene;
using Domain.IPresenter.Util.Input;
using Module.Singleton;
using UnityEngine;

namespace Installer
{
    public class GlobalLocator: SingletonMonoBehaviour<GlobalLocator>
    {
        protected override void Awake()
        {
            base.Awake();
            // View
            var inputView = new InputView();
            var sceneLoadView = new SceneLoadView();

            // Presenter
            var sceneLoadPresenter = new SceneLoadPresenter(sceneLoadView);
            var fingerEventPresenter = new FingerEventPresenter(inputView);
            Register<IScenePresenter>(sceneLoadPresenter);
            Register<IFingerTouchEventPresenter>(fingerEventPresenter);
            Register<IFingerTouchingEventPresenter>(fingerEventPresenter);
            Register<IFingerReleaseEventPresenter>(fingerEventPresenter);

            _tickables = new[]
            {
                (ITickable)inputView, fingerEventPresenter
            };
        }

        private static Dictionary<Type, object> InstanceDictionary { get; } = new Dictionary<Type, object>();
        private static ITickable[] _tickables;

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            for (int i = 0; i < _tickables.Length; i++)
            {
                _tickables[i].Tick(deltaTime);
            }
        }

        private static void Register<T>(T instance) where T : class
        {
            InstanceDictionary[typeof(T)] = instance;
        }

        public static T Resolve<T>() where T : class
        {
            if (InstanceDictionary.TryGetValue(typeof(T), out var instance))
            {
                return instance as T;
            }

            Debug.LogWarning($"Locator: {typeof(T).Name} not found");
            return null;
        }
    }
}