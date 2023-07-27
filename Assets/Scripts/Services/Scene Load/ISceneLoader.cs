using System;

namespace Features.SceneLoading.Scripts
{
    public interface ISceneLoader
    {
        void Load(int index, Action onLoaded = null);
    }
}