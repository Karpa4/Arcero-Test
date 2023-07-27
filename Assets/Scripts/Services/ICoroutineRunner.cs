using System.Collections;

namespace Features.Services.Coroutine
{
  public interface ICoroutineRunner
  {
    UnityEngine.Coroutine StartCoroutine(IEnumerator coroutine);
    void StopAllCoroutines();
    void StopCoroutine(UnityEngine.Coroutine coroutineRunner);
  }
}