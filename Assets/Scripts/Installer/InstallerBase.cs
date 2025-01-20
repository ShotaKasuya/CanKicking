using System;
using System.Collections.Generic;
using DataUtil.Util.Installer;
using UnityEngine;
using Time = UnityEngine.Time;

namespace Installer
{
    public abstract class InstallerBase : MonoBehaviour, IDisposable
    {
        private InstallerStateType _stateType = InstallerStateType.NotConfigured;

        private void Awake()
        {
            Configure();
        }

        private void Configure()
        {
            if (_stateType == InstallerStateType.AlreadyConfigured)
            {
                return;
            }

            _stateType = InstallerStateType.AlreadyConfigured;
            CustomConfigure();
        }

        protected virtual void CustomConfigure()
        {
        }

        private HashSet<IDisposable> Disposables { get; } = new HashSet<IDisposable>();
        private HashSet<ITickable> Tickables { get; } = new HashSet<ITickable>();
        private Dictionary<Type, object> InstanceDictionary { get; } = new Dictionary<Type, object>();

        /// <summary>
        /// 登録された公開インスタンスを取得する
        /// </summary>
        /// <typeparam name="T">取得するインスタンスの型</typeparam>
        /// <returns>インスタンス</returns>
        /// <exception cref="KeyNotFoundException">インスタンスが登録されていない</exception>
        public T GetInstance<T>() where T : class
        {
            if (_stateType != InstallerStateType.AlreadyConfigured)
            {
                Configure();
            }

            if (InstanceDictionary.TryGetValue(typeof(T), out var instance))
            {
                return (T)instance;
            }

            Debug.LogError($"Instance {typeof(T)} not found");
            
            // 関数が死ぬとインスタンス登録漏れが1つしか出ないので実行を続ける
            return null;
        }

        /// <summary>
        /// 公開インスタンスを登録する
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TContract">抽象型</typeparam>
        /// <typeparam name="TConcrete">具現型</typeparam>
        protected void RegisterInstance<TContract, TConcrete>(TConcrete instance) where TConcrete : TContract
        {
            InstanceDictionary[typeof(TContract)] = instance;
        }

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