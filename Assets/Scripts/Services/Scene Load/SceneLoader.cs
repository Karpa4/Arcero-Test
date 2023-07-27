using System;
using System.Collections;
using Features.Services.Coroutine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Features.SceneLoading.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner coroutineRunner;
        private readonly LoadingCurtain loadingCurtain;

        private Coroutine loadingCoroutine;

        [Inject]
        public SceneLoader(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            this.coroutineRunner = coroutineRunner;
            this.loadingCurtain = loadingCurtain;
        }

        public void Load(int index, Action onLoaded)
        {
            if (loadingCoroutine != null)
                coroutineRunner.StopCoroutine(loadingCoroutine);
            loadingCoroutine = coroutineRunner.StartCoroutine(LoadScene(index, onLoaded));
        }

        private IEnumerator LoadScene(int index, Action onLoaded = null)
        {
            loadingCurtain.Show();
            while (loadingCurtain.IsShown == false)
                yield return null;

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(index);

            while (waitNextScene.isDone == false)
                yield return null;

            loadingCurtain.Hide();

            onLoaded?.Invoke();
            loadingCoroutine = null;
        }
    }
}
