using System;

namespace Jab.Unity.Injectors
{
    public interface IInjector
    {
        void Inject(object toInject, IServiceProvider serviceProvider);
    }
}