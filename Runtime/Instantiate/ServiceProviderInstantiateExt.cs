using System;
using Jab.UnityExtensions.Enums;
using Jab.UnityExtensions.Injectors;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Jab.UnityExtensions
{
    public static class ServiceProviderInstantiateExt
    {
        private static Transform _disabledParent;
        
        [UsedImplicitly]
        public static GameObject Instantiate<TServiceProvider>(this TServiceProvider serviceProvider, IInstantiateParams instantiateParams)
            where TServiceProvider : IServiceProvider<IInjector>, IServiceProvider
        {
            return serviceProvider.GetService().Instantiate(instantiateParams, serviceProvider);
        }
        
        [UsedImplicitly]
        public static GameObject Instantiate(this IInjector injector, IInstantiateParams instantiateParams, IServiceProvider serviceProvider)
        {
            GameObject instantiated;
            
            using (InstantiateDisabled(instantiateParams, out instantiated))
                injector.InjectGameObject(instantiated, serviceProvider, instantiateParams.InjectionMode);

            return instantiated;
        }
        
        [UsedImplicitly]
        public static T Instantiate<TServiceProvider, T>(this TServiceProvider serviceProvider, IInstantiateParams instantiateParams) 
            where TServiceProvider : IServiceProvider<IInjector>, IServiceProvider
            where T : Component
        {
            T instantiated;
            
            using (InstantiateDisabled(instantiateParams, out instantiated))
                serviceProvider.InjectGameObject(instantiated.gameObject, instantiateParams.InjectionMode);

            return instantiated;
        }
        
        [UsedImplicitly]
        public static T Instantiate<T>(this IInjector injector, IInstantiateParams instantiateParams, IServiceProvider serviceProvider) 
            where T : Component
        {
            T instantiated;
            
            using (InstantiateDisabled(instantiateParams, out instantiated))
                injector.InjectGameObject(instantiated.gameObject, serviceProvider, instantiateParams.InjectionMode);

            return instantiated;
        }

        private static IDisposable InstantiateDisabled<T>(IInstantiateParams instantiateParams, out T instantiated) 
            where T : Object
        {
            var parent = instantiateParams.Parent;
        switch (instantiateParams.InstantiateSourceMode, instantiateParams.SourceGameObject.activeSelf)
            {
                case (InstantiateSourceMode.Prefab, false):
                    instantiated = Instantiate((T)instantiateParams.SourceObject, instantiateParams, parent);
                    return null;
                case (InstantiateSourceMode.Prefab, true):
                    instantiateParams.SourceGameObject.SetActive(false);
                    instantiated = Instantiate((T)instantiateParams.SourceObject, instantiateParams, parent);
                    return new EnableOnDispose(instantiateParams.SourceGameObject, GetGameObject(instantiated));
                case (InstantiateSourceMode.ExistingGameObjectInstance, false):
                    instantiated = Instantiate((T)instantiateParams.SourceObject, instantiateParams, parent);
                    return null;
                case (InstantiateSourceMode.ExistingGameObjectInstance, true):
                    //We cannot disable an existing instance, it will invoke all Disable/Enable messages
                    //So instead we instantiate it under a different disabled object, inject it and them transferring it to it's rightful parent 
                    if (_disabledParent == null)
                    {
                        _disabledParent = new GameObject
                        {
                            name = "DisabledParent",
                            hideFlags = HideFlags.HideAndDontSave
                        }.transform;
                        _disabledParent.gameObject.SetActive(false);
                    }

                    instantiated = Instantiate((T)instantiateParams.SourceObject, instantiateParams, _disabledParent);
                    return new SetParentOnDispose(GetGameObject(instantiated).transform, parent);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private static T Instantiate<T>(T source, IInstantiateParams instantiateParams, Transform parent) 
            where T : Object
        {
            if (instantiateParams.PositionAndRotation.HasValue)
            {
                var position = instantiateParams.PositionAndRotation.Value.position;
                var rotation = instantiateParams.PositionAndRotation.Value.rotation;
                
                if (parent != null)
                    return Object.Instantiate(source, position, rotation, parent);

                return Object.Instantiate(source, position, rotation);
            }

            if (parent != null)
                return Object.Instantiate(source, parent);

            return Object.Instantiate(source);
        }

        private static GameObject GetGameObject(Object obj)
        {
            return obj switch
            {
                GameObject gameObject => gameObject,
                Component component => component.gameObject,
                _ => throw new ArgumentOutOfRangeException($"{nameof(obj)} is not a gameobject or component")
            };
        }
        
        private class EnableOnDispose : IDisposable
        {
            private readonly GameObject _source;
            private readonly GameObject _instantiated;
            private bool _disposed;

            public EnableOnDispose(GameObject source, GameObject instantiated)
            {
                _source = source;
                _instantiated = instantiated;
            }

            ~EnableOnDispose()
            {
                Dispose();
            }
            
            public void Dispose()
            {
                if(_disposed)
                    return;
                
                _source.SetActive(true);
                _instantiated.SetActive(true);
                
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }
        
        private class SetParentOnDispose : IDisposable
        {
            private readonly Transform _instantiated;
            private readonly Transform _parent;
            private bool _disposed;

            public SetParentOnDispose(Transform instantiated, Transform parent)
            {
                _instantiated = instantiated;
                _parent = parent;
            }

            ~SetParentOnDispose()
            {
                Dispose();
            }
            
            public void Dispose()
            {
                if(_disposed)
                    return;
                
                _instantiated.SetParent(_parent);
                
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}