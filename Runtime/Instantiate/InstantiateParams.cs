using Jab.UnityExtensions.Enums;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Jab.UnityExtensions
{
    public interface IInstantiateParams
    {
        Object SourceObject { get; }
        GameObject SourceGameObject { get; }
        InstantiateSourceMode InstantiateSourceMode { get; }
        Transform Parent { get; }
        (Vector3 position, Quaternion rotation)? PositionAndRotation { get; }
        InjectionMode InjectionMode { get; }
    }

    public readonly struct InstantiateParams : IInstantiateParams
    {
        public InstantiateParams(
            GameObject source,
            InstantiateSourceMode instantiateSourceMode, 
            Transform parent = null, 
            (Vector3 position, Quaternion rotation)? positionAndRotation = null, 
            InjectionMode injectionMode = InjectionMode.Recursive)
        {
            SourceGameObject = source;
            Parent = parent;
            PositionAndRotation = positionAndRotation;
            InstantiateSourceMode = instantiateSourceMode;
            InjectionMode = injectionMode;
        }

        public Object SourceObject => SourceGameObject;
        public GameObject SourceGameObject { get; }
        public InstantiateSourceMode InstantiateSourceMode { get; }
        public Transform Parent { get; }
        public (Vector3 position, Quaternion rotation)? PositionAndRotation { get; }
        public InjectionMode InjectionMode { get; }
    } 
    
    public readonly struct InstantiateParams<T> : IInstantiateParams 
        where T : Behaviour
    {
        public InstantiateParams(
            T source, 
            InstantiateSourceMode instantiateSourceMode, 
            Transform parent = null, 
            (Vector3 position, Quaternion rotation)? positionAndRotation = null, 
            InjectionMode injectionMode = InjectionMode.Recursive)
        {
            Source = source;
            Parent = parent;
            PositionAndRotation = positionAndRotation;
            InstantiateSourceMode = instantiateSourceMode;
            InjectionMode = injectionMode;
        }

        public T Source { get; }
        public Object SourceObject => Source;
        public GameObject SourceGameObject => Source.gameObject;
        public InstantiateSourceMode InstantiateSourceMode { get; }
        public Transform Parent { get; }
        public (Vector3 position, Quaternion rotation)? PositionAndRotation { get; }
        public InjectionMode InjectionMode { get; }
    }
}