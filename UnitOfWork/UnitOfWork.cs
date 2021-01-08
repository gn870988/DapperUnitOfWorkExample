using System;
using System.Data;
using UnitOfWorks.Infrastructure;
using UnitOfWorks.Repositories;

namespace UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IMemberRepository _memberRepository;
        private bool _disposed;

        public UnitOfWork(Dialect dialect, ConnectionSelect connectionSelect)
        {
            var factory = new ConnectionFactory(dialect, connectionSelect);

            _connection = factory.CreateDbConn();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IMemberRepository MemberRepository => 
            _memberRepository ?? (_memberRepository = new MemberRepository(_transaction));

        public void Complete()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _memberRepository = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
