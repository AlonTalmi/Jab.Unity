using System;

namespace Jab.Unity.Exceptions
{
    internal sealed class FieldInjectorException : Exception
    {
        public FieldInjectorException(Exception e) : base(e.Message)
        {
        }
    }
}