using System;

namespace Jab.UnityExtensions.ManualInjector
{
    public interface IInjectable
    {
        void Inject(IServiceProvider serviceProvider);
    }
}