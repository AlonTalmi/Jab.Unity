using System;
using Jab.UnityExtensions.Enums;
using Jab.UnityExtensions.Injectors;
using UnityEngine;

namespace Jab.UnityExtensions.Extensions
{
    public static class InstantiateParamsFactoryExt
    {
        public static InstantiateParamsFactory StartInstantiate<TServiceProvider>(this TServiceProvider serviceProvider, GameObject source, InstantiateSourceMode instantiateSourceMode)
            where TServiceProvider : IServiceProvider<IInjector>, IServiceProvider
        {
            return new(serviceProvider, source, instantiateSourceMode);
        }
        
        public static InstantiateParamsFactory<T> StartInstantiate<TServiceProvider, T>(this TServiceProvider serviceProvider, T source, InstantiateSourceMode instantiateSourceMode)
            where TServiceProvider : IServiceProvider<IInjector>, IServiceProvider
            where T : Behaviour
        {
            return new(serviceProvider, source, instantiateSourceMode);
        }

        public static GameObject Instantiate(this InstantiateParamsFactory instantiateParamsFactory)
        {
            var injector = (IInjector)instantiateParamsFactory.ServiceProvider.GetService(typeof(IInjector));
            return injector.Instantiate(instantiateParamsFactory.Create(), instantiateParamsFactory.ServiceProvider);
        }
        
        public static T Instantiate<T>(this InstantiateParamsFactory<T> instantiateParamsFactory) 
            where T : Behaviour
        {
            var injector = (IInjector)instantiateParamsFactory.ServiceProvider.GetService(typeof(IInjector));
            return injector.Instantiate<T>(instantiateParamsFactory.Create(), instantiateParamsFactory.ServiceProvider);
        }
    }
}