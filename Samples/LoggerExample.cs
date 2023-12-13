using Jab.UnityExtensions.Enums;
using Jab.UnityExtensions.Extensions;
using Jab.UnityExtensions.Injectors;
using UnityEngine;

[DefaultExecutionOrder(-20)]
public class LoggerExample : MonoBehaviour
{
    [SerializeField] private ServiceProvider _serviceProvider;
    [SerializeField] private InjectableComponent _toInject;
    [SerializeField] private GameObject _instantiateFromExisting;
    [SerializeField] private GameObject _instantiateFromPrefab;
    
    private void Awake()
    {
        var str = _serviceProvider.GetService<string>();
        _serviceProvider.GetService<Logger>().Log(str);
        _serviceProvider.Inject(_toInject);

        _serviceProvider
            .StartInstantiate(_instantiateFromExisting, InstantiateSourceMode.ExistingGameObjectInstance)
            .WithParent(transform)
            .Instantiate();
        
        _serviceProvider
            .StartInstantiate(_instantiateFromPrefab, InstantiateSourceMode.Prefab)
            .WithParent(transform)
            .Instantiate();
    }
}
