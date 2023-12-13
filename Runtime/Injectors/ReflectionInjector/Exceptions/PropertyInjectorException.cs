using System;

namespace Jab.UnityExtensions.Exceptions
{
    internal sealed class PropertyInjectorException : Exception
    {
        public PropertyInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}