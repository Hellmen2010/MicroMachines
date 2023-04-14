using System;
using MicroMachines.Code.Infrastructure.ServiceContainer;

namespace MicroMachines.Code.Services.EntityContainer
{
    public interface IEntityContainer : IService, IDisposable
    {
        void RegisterEntity<TEntity>(TEntity entity) where TEntity : class;
        TEntity GetEntity<TEntity>();
    }
}