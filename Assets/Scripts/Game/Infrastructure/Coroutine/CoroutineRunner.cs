using System.Collections;
using UnityEngine;

namespace Game.Infrastructure.Coroutine
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner 
    {
        public new UnityEngine.Coroutine StartCoroutine(IEnumerator routine)
        {
            return base.StartCoroutine(routine);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}