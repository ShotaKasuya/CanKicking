using System;
using System.Collections.Generic;
using Adapter.Presenter.Scene;
using Detail.View.Scene;
using Domain.IPresenter.Scene;
using UnityEngine;

namespace Installer
{
    public static class GlobalLocator
    {
        static GlobalLocator()
        {
            // View
            var sceneLoadView = new SceneLoadView();

            // Presenter
            var sceneLoadPresenter = new SceneLoadPresenter(sceneLoadView);
            
            Register<IScenePresenter>(sceneLoadPresenter);
        }
        
        private static Dictionary<Type, object> InstanceDictionary { get; } = new Dictionary<Type, object>();

        public static void Register<T>(T instance) where T : class
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