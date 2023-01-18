using Abp.Domain.Uow;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using uCondo.HandsOn.Infrastructure.Interfaces;

namespace uCondo.HandsOn.Infrastructure.Implamentations
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SqlConnection _sqlConn;
        private SqlTransaction _sqlTrans;
        private readonly string _connectionString;
        private readonly RepositoryConfiguration _config;

        protected RepositoryBase(RepositoryConfiguration config)
        {
            _config = config;
            _sqlConn = new SqlConnection(config.ConnectionString);
            _connectionString = config.ConnectionString;
        }

        event EventHandler<UnitOfWorkFailedEventArgs> IActiveUnitOfWork.Failed
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public virtual async Task OpenConnectionAsync()
        {
            if (string.IsNullOrWhiteSpace(_sqlConn.ConnectionString) && !string.IsNullOrWhiteSpace(_connectionString))
                _sqlConn.ConnectionString = _connectionString;

            if (_sqlConn.State == ConnectionState.Closed)
            {
                _sqlConn = new SqlConnection(_config.ConnectionString);
                await _sqlConn.OpenAsync();
            }

        }

        public virtual async Task<IEnumerable<T>> CallStoredProcedureAsync(string name, DynamicParameters parameters = null)
        {
            _sqlConn = new SqlConnection(_config.ConnectionString);
            await OpenConnectionAsync();
            await using var conn = _sqlConn;

            var results = conn.Query<T>(name, parameters, commandType: CommandType.StoredProcedure).ToList();
            return results;
        }

        public event EventHandler Completed;
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;
        public event EventHandler Disposed;

        public string Id => throw new NotImplementedException();

        public IUnitOfWork Outer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UnitOfWorkOptions Options => throw new NotImplementedException();

        public IReadOnlyList<DataFilterConfiguration> Filters => throw new NotImplementedException();

        public IReadOnlyList<AuditFieldConfiguration> AuditFieldConfiguration => throw new NotImplementedException();

        public Dictionary<string, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsDisposed => throw new NotImplementedException();

        IUnitOfWork IUnitOfWork.Outer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        UnitOfWorkOptions IActiveUnitOfWork.Options => throw new NotImplementedException();

        IReadOnlyList<DataFilterConfiguration> IActiveUnitOfWork.Filters => throw new NotImplementedException();

        IReadOnlyList<AuditFieldConfiguration> IActiveUnitOfWork.AuditFieldConfiguration => throw new NotImplementedException();

        public Task<int> Execute(string query, DynamicParameters parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> QuerySingleAsync(string query, DynamicParameters parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync(string query, DynamicParameters parameters = null)
        {
            throw new NotImplementedException();
        }

        public void Begin(UnitOfWorkOptions options)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public IDisposable DisableFilter(params string[] filterNames)
        {
            throw new NotImplementedException();
        }

        public IDisposable EnableFilter(params string[] filterNames)
        {
            throw new NotImplementedException();
        }

        public bool IsFilterEnabled(string filterName)
        {
            throw new NotImplementedException();
        }

        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            throw new NotImplementedException();
        }

        public IDisposable DisableAuditing(params string[] fieldNames)
        {
            throw new NotImplementedException();
        }

        public IDisposable EnableAuditing(params string[] fieldNames)
        {
            throw new NotImplementedException();
        }

        public IDisposable SetTenantId(int? tenantId)
        {
            throw new NotImplementedException();
        }

        public IDisposable SetTenantId(int? tenantId, bool switchMustHaveTenantEnableDisable)
        {
            throw new NotImplementedException();
        }

        public int? GetTenantId()
        {
            throw new NotImplementedException();
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _sqlConn?.Dispose();
            _sqlTrans?.Dispose();
        }

        public Task CallStoreProcedureAsync(string name, DynamicParameters parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}