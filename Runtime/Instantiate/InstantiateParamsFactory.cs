using System;
using UnityEngine;

namespace Jab.Unity
{
    public class InstantiateParamsFactory
    {
        public readonly GameObject Source;
        
        public readonly IServiceProvider ServiceProvider;
        public readonly InstantiateSourceMode InstantiateSourceMode;
        public Transform Parent { get; private set; }
        public (Vector3 position, Quaternion rotation)? Position { get; private set; }
        public InjectionMode InjectionMode { get; private set; } = InjectionMode.Recursive;

        public InstantiateParamsFactory(IServiceProvider serviceProvider, GameObject source, InstantiateSourceMode instantiateSourceMode)
        {
            Source = source;
            InstantiateSourceMode = instantiateSourceMode;
            ServiceProvider = serviceProvider;
        }

        public InstantiateParamsFactory WithParent(Transform parent)
        {
            Parent = parent;
            return this;
        }

        public InstantiateParamsFactory WithPosition(Vector3 position, Quaternion rotation)
        {
            Position = (position, rotation);
            return this;
        }

        public InstantiateParamsFactory WithInjectionMode(InjectionMode injectionMode)
        {
            InjectionMode = injectionMode;
            return this;
        }

        public IInstantiateParams Create()
        {
            return new InstantiateParams(
                Source, 
                InstantiateSourceMode, 
                Parent, 
                Position, 
                InjectionMode);
        }
    }

    public class InstantiateParamsFactory<T>
        where T : Behaviour
    {
        public readonly T Source;
        
        public readonly IServiceProvider ServiceProvider;
        public readonly InstantiateSourceMode InstantiateSourceMode;
        public Transform Parent { get; private set; }
        public (Vector3 position, Quaternion rotation)? Position { get; private set; }
        public InjectionMode InjectionMode { get; private set; } = InjectionMode.Recursive;

        public InstantiateParamsFactory(IServiceProvider serviceProvider, T source, InstantiateSourceMode instantiateSourceMode)
        {
            Source = source;
            InstantiateSourceMode = instantiateSourceMode;
            ServiceProvider = serviceProvider;
        }

        public InstantiateParamsFactory<T> WithParent(Transform parent)
        {
            Parent = parent;
            return this;
        }

        public InstantiateParamsFactory<T> WithPosition(Vector3 position, Quaternion rotation)
        {
            Position = (position, rotation);
            return this;
        }

        public InstantiateParamsFactory<T> WithInjectionMode(InjectionMode injectionMode)
        {
            InjectionMode = injectionMode;
            return this;
        }

        public IInstantiateParams Create()
        {
            return new InstantiateParams<T>(
                Source, 
                InstantiateSourceMode, 
                Parent, 
                Position, 
                InjectionMode);
        }
    }
}