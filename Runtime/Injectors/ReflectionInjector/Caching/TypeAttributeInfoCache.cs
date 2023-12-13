using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jab.UnityExtensions.Caching
{
    public static class ReflectionTypeInfoCache
    {
        private static readonly Dictionary<Type, TypeAttributeInfo> Dictionary = new();
        
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static TypeAttributeInfo Get(Type type)
        {
            if (Dictionary.TryGetValue(type, out var info)) 
                return info;
            
            info = Generate(type);
            Dictionary.Add(type, info);

            return info;
        }
        
        private static TypeAttributeInfo Generate(IReflect type)
        {
            var fields = type
                .GetFields(Flags)
                .Where(f => f.IsDefined(typeof(InjectAttribute)))
                .ToArray();
            
            var properties = type
                .GetProperties(Flags)
                .Where(p => p.CanWrite && p.IsDefined(typeof(InjectAttribute)))
                .ToArray();
            
            var methods = type
                .GetMethods(Flags)
                .Where(m => m.IsDefined(typeof(InjectAttribute)))
                .ToArray();

            return new(fields, properties, methods);
        }
    }
}