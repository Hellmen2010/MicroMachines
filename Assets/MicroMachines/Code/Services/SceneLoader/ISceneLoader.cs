using System;
using MicroMachines.Code.Infrastructure.ServiceContainer;

namespace MicroMachines.Code.Services.SceneLoader
{
    public interface ISceneLoader : IService
    {
        void LoadScene(string sceneName, Action onLoaded = null);
    }
}