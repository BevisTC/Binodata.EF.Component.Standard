using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Binodata.EF.Component.Standard.Repository
{
    public class EFGenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly DbContext _entities;

        /// <summary>
        /// disposable flag
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// 建構 constructor
        /// </summary>
        /// <param name="context">entity framework db context</param>
        public EFGenericRepository(DbContext context)
        {
            this._entities = context;
        }

        /// <summary>
        /// add data
        /// </summary>
        /// <param name="entity">data</param>
        public void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        /// <summary>
        /// delete data
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            _entities.Entry(entity).State = EntityState.Deleted;
            _entities.Set<T>().Remove(entity);
        }

        /// <summary>
        /// edit 
        /// </summary>
        /// <param name="entity"></param>
        public void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }


        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    DisposeDbContext();
                }
            }
            this._disposed = true;
        }

        public int Save()
        {
            var entities = _entities.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return _entities.SaveChanges();

        }

        private void DisposeDbContext()
        {
            _entities?.Dispose();
        }
    }

}
