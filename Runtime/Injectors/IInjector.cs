using System;

namespace Jab.UnityExtensions.Injectors
{
    public interface IInjector
    {
        void Inject(object toInject, IServiceProvider serviceProvider);
    }
}