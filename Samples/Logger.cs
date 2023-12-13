using UnityEngine;

namespace Jab.Unity.Samples
{
    public class Logger : MonoBehaviour
    {
        public void Log(string message)
        {
            Debug.Log(message);
        } 

        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}