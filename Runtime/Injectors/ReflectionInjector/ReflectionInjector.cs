using System;
using System.Collections.Generic;
using System.Reflection;
using Jab.UnityExtensions.Buffers;
using Jab.UnityExtensions.Caching;
using Jab.UnityExtensions.Exceptions;

namespace Jab.UnityExtensions.Injectors
{
    public class ReflectionInjector : IInjector
    { 
        public void Inject(object toInject, IServiceProvider serviceProvider)
        {
            var info = ReflectionTypeInfoCache.Get(toInject.GetType());
            Inject(info.InjectableFields, toInject, serviceProvider);
            Inject(info.InjectableProperties, toInject, serviceProvider);
            Inject(info.InjectableMethods, toInject, serviceProvider);
        }

        private static void Inject(IEnumerable<FieldInfo> fields, object instance, IServiceProvider serviceProvider)
        {
            foreach (var field in fields) 
                Inject(field, instance, serviceProvider);
        }
        
        private static void Inject(FieldInfo field, object instance, IServiceProvider container)
        {
            try
            {
                field.SetValue(instance, container.GetService(field.FieldType));
            }
            catch (Exception e)
            {
                throw new FieldInjectorException(e);
            }
        }

        private static void Inject(IEnumerable<PropertyInfo> properties, object instance, IServiceProvider serviceProvider)
        {
            foreach (var field in properties) 
                Inject(field, instance, serviceProvider);
        }
        
        private static void Inject(PropertyInfo property, object instance, IServiceProvider serviceProvider)
        {
            try
            {
                property.SetValue(instance, serviceProvider.GetService(property.PropertyType));
            }
            catch (Exception e)
            {
                throw new PropertyInjectorException(e);
            }
        }

        private static void Inject(IEnumerable<InjectedMethodInfo> methods, object instance, IServiceProvider serviceProvider)
        {
            foreach (var t in methods) 
                Inject(t, instance, serviceProvider);
        }
        
        private static void Inject(InjectedMethodInfo method, object instance, IServiceProvider serviceProvider)
        {
            var arguments = ExactArrayPool<object>.Shared.Rent(method.Parameters.Length);

            for (var i = 0; i < method.Parameters.Length; i++) 
                arguments[i] = serviceProvider.GetService(method.Parameters[i].ParameterType);

            try
            {
                method.MethodInfo.Invoke(instance, arguments);
            }
            catch (Exception e)
            {
                throw new MethodInjectorException(instance, method.MethodInfo, e);
            }
            finally
            {
                ExactArrayPool<object>.Shared.Return(arguments);
            }
        }
    }
}