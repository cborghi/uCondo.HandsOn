using Abp.Domain.Uow;
using Dapper;

namespace uCondo.HandsOn.Infrastructure.Interfaces
{
    public interface IRepositoryBase<T> : IDisposable, IUnitOfWork
    {
        public Task CallStoreProcedureAsync(string name, DynamicParameters parameters = null);
    }
}
