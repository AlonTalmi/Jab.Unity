using System;

namespace Jab.Unity.Injectors
{
    public class CombineInjectors : IInjector
    {
        private readonly IInjector[] _injectors;

        public CombineInjectors(params IInjector[] injectors)
        {
            _injectors = injectors;
        }

        public void Inject(object toInject, IServiceProvider serviceProvider)
        {
            foreach (var injector in _injectors) 
                injector.Inject(toInject, serviceProvider);
        }
    }
}
