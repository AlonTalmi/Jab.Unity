using System;

namespace Jab.UnityExtensions.Exceptions
{
    internal sealed class FieldInjectorException : Exception
    {
        public FieldInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}