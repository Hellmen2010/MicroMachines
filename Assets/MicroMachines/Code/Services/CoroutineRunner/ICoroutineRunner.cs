using System.Collections;
using MicroMachines.Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace MicroMachines.Code.Services.CoroutineRunner
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}