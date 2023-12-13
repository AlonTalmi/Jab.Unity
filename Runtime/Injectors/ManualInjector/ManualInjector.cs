using System;
using Jab.Unity.Injectors;

namespace Jab.Unity.Injectors
{
    public class ManualInjector : IInjector
    {
        public void Inject(object toInject, IServiceProvider serviceProvider)
        {
            if (toInject is IInjectable injectable)
                injectable.Inject(serviceProvider);
        }
    }
}