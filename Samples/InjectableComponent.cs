using System;
using Jab.Unity.Injectors;
using UnityEngine;

namespace Jab.Unity.Samples
{
    public class InjectableComponent : MonoBehaviour, IInjectable
    {
        [Inject] private readonly string _message1;
        private string _message2;

        private void Awake()
        {
            Debug.Log(_message1, this);
            Debug.Log(_message2, this);
        }

        public void Inject(IServiceProvider serviceProvider)
        {
            _message2 = serviceProvider.GetService<string>();
        }
    }
}