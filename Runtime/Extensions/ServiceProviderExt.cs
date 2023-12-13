using System;

namespace Jab.UnityExtensions
{
    public static class ServiceProviderExt
    {
        public static T GetService<T>(this IServiceProvider serviceProvider) => (T)serviceProvider.GetService(typeof(T));
    }
}