using System;

namespace Jab.Unity.Injectors
{
    public interface IInjectable
    {
        void Inject(IServiceProvider serviceProvider);
    }
}