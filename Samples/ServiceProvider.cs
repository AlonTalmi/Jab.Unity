using Jab.Unity.Injectors;
using UnityEngine;

namespace Jab.Unity.Samples
{
    [ServiceProvider]
    [Singleton(typeof(Logger), Instance = nameof(_logger))]
    [Singleton(typeof(IInjector), Factory = "CreateInjector")]
    [Singleton(typeof(string), Instance = nameof(_message))]
    public partial class ServiceProvider : MonoBehaviour
    {
        [SerializeField] private Logger _logger;
        [SerializeField] private string _message;

        private static IInjector CreateInjector()
        {
            return new CombineInjectors(
                new ReflectionInjector(),
                new ManualInjector()
            );
        }
    }
}