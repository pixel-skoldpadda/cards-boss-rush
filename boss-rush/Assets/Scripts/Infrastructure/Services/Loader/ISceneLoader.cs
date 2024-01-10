using System;
using System.Collections;

namespace Infrastructure.Services.Loader
{
    public interface ISceneLoader
    {
        void Init(ICoroutineRunner coroutineRunner);
        void Load(string name, Action onLoaded = null);
        IEnumerator LoadScene(string nextScene, Action onLoaded = null);
    }
}