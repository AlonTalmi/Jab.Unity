using System;

namespace Jab.Unity
{
    public static class ServiceProviderExt
    {
        public static T GetService<T>(this IServiceProvider serviceProvider) => (T)serviceProvider.GetService(typeof(T));
    }
}