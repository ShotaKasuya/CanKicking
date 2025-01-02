using System;
using System.Collections.Generic;
using DataUtil.Util.Installer;
using UnityEngine;

namespace Installer
{
    public abstract class InstallerBase: MonoBehaviour, IDisposable
    {
        private HashSet<IDisposable> Disposables { get; } = new HashSet<IDisposable>();
        private HashSet<ITickable> Tickables{ get; } = new HashSet<ITickable>();
        
        protected void RegisterEntryPoints(object instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            
            RegisterTickable(instance);
            RegisterDisposable(instance);
        }

        private void RegisterTickable(object instance)
        {
            if (instance is ITickable tickable)
            {
                Tickables.Add(tickable);
            }
        }

        private void RegisterDisposable(object instance)
        {
            if (instance is IDisposable disposable)
            {
                Disposables.Add(disposable);
            }
        }
        
        
        private void Update()
        {
            var dt = Time.deltaTime;
            foreach (var tickable in Tickables)
            {
                tickable.Tick(dt);
            }
        }

        public void Dispose()
        {
            foreach (var disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
    }
}