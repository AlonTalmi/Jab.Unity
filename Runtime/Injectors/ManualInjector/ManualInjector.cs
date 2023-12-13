using System;
using Jab.UnityExtensions.Injectors;

namespace Jab.UnityExtensions.ManualInjector
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