using Dapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace UnitOfWorks.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : EntityIdentify
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction.Connection;

        public GenericRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public TEntity Get(int id)
        {
            return Connection.Get<TEntity>(id, Transaction);
        }

        public TEntity Get(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            return Get(entity.Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Connection.GetList<TEntity>(String.Empty, transaction: Transaction).ToList();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = default(int);
            entity.Id = Connection.Insert(entity, Transaction) ?? default(int);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Connection.Update(entity, Transaction);
        }

        public void Remove(int id)
        {
            Connection.Delete<TEntity>(id, Transaction);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Remove(entity.Id);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        /// <summary>
        /// 取TEntity的TableName
        /// </summary>
        protected string GetTableNameMapper()
        {
            dynamic attributeTable = typeof(TEntity).GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType().Name == "TableAttribute");

            return attributeTable != null ? attributeTable.Name : typeof(TEntity).Name;
        }
    }
}
