using System.Collections;

namespace Game.Infrastructure.Coroutine
{
    public interface ICoroutineRunner
    {
        UnityEngine.Coroutine StartCoroutine(IEnumerator routine);
    }
}