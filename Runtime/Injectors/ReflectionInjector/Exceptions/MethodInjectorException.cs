using System;
using System.Linq;
using System.Reflection;

namespace Jab.Unity.Exceptions
{
    internal sealed class MethodInjectorException : Exception
    {
        public MethodInjectorException(object obj, MethodBase method, Exception e) : base(BuildMessage(obj, method, e))
        {
        }

        private static string BuildMessage(object obj, MethodBase method, Exception e)
        {
            var parameters = method.GetParameters();
            var methodDescription = $"'{obj.GetType().Name}::{method.Name}'";
            var parametersDescription = $"'{string.Join(", ", parameters.Select(p => p.ParameterType.Name))}'";
            return $"Could not inject method {methodDescription} with parameters {parametersDescription}\n\n{e}";
        }
    }
}