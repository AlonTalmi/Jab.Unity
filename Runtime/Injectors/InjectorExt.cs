using System;
using System.Collections.Generic;
using System.Linq;
using Jab.Unity.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jab.Unity.Injectors
{
    public static class InjectorExt
    {
        [UsedImplicitly]
        public static void Inject<TServiceProvider>(this TServiceProvider serviceProvider, object toInject)
            where TServiceProvider : IServiceProvider<IInjector>, IServiceProvider
        {
            serviceProvider.GetService().Inject(toInject, serviceProvider);
        }

        [UsedImplicitly]
        public static void Inject(this IInjector injector, object toInject, IServiceProvider serviceProvider)
        {
            injector.Inject(toInject, serviceProvider);
        }

        [UsedImplicitly]
        public static void InjectScene<TServiceProvider>(this TServiceProvider serviceProvider, Scene scene)
            where TServiceProvider : IServiceProvider<IInjector>, IServiceProvider
        {
            serviceProvider.GetService().Inject(scene, serviceProvider);
        }

        [UsedImplicitly]
        public static void InjectScene(this IInjector injector, Scene scene, IServiceProvider serviceProvider)
        {
            var monoBehaviours = scene
                .GetRootGameObjects()
                .SelectMany(gameObject => gameObject.GetComponentsInChildren<MonoBehaviour>(true))
                .Where(m => m != null);

            foreach (var monoBehaviour in monoBehaviours)
                injector.Inject(monoBehaviour, serviceProvider);
        }

        [UsedImplicitly]
        public static void InjectGameObject<TServiceProvider>(this TServiceProvider serviceProvider, GameObject gameObject, InjectionMode injectionMode = InjectionMode.Recursive)
            where TServiceProvider : IServiceProvider<IInjector>, IServiceProvider
        {
            serviceProvider.GetService().InjectGameObject(gameObject, serviceProvider, injectionMode);
        }

        [UsedImplicitly]
        public static void InjectGameObject(this IInjector injector, GameObject gameObject, IServiceProvider serviceProvider, InjectionMode injectionMode = InjectionMode.Recursive)
        {
            foreach (var toInject in gameObject.GetInjectables(injectionMode))
                injector.Inject(toInject, serviceProvider);
        }

        [UsedImplicitly]
        private static IEnumerable<MonoBehaviour> GetInjectables(this GameObject gameObject, InjectionMode injectionMode)
        {
            return injectionMode switch
            {
                InjectionMode.Single => gameObject.GetComponent<MonoBehaviour>().Yield(),
                InjectionMode.Object => gameObject.GetComponents<MonoBehaviour>(),
                InjectionMode.Recursive => gameObject.GetComponentsInChildren<MonoBehaviour>(true),
                _ => throw new ArgumentOutOfRangeException(nameof(injectionMode), injectionMode, null)
            };
        }
    }
}