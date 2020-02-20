using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Binodata.EF.Component.Standard.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Binodata.EF.Component.Standard
{
    public class EFUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// db context
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// 
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        private Hashtable _repositories;

        public EFUnitOfWork(DbContext dbContext)
        {
            this._context = dbContext;
        }

        /// <summary>
        /// 記憶體 拋棄
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Create Generic Repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IGenericRepository<T> GetGenericRepository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type)) return (IGenericRepository<T>) _repositories[type];
            var repositoryType = typeof(EFGenericRepository<>);

            var repositoryInstance =
                Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(T)), _context);

            _repositories.Add(type, repositoryInstance);

            return (IGenericRepository<T>)_repositories[type];
        }

        /// <summary>
        /// save changes
        /// </summary>
        public int Save()
        {
            var entities = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return _context.SaveChanges();

        }

        /// <summary>
        /// 回收物件
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ClearRepositories();
                    DisposeDbContext();
                }
            }

            _disposed = true;
        }

        private void ClearRepositories()
        {
            if (_repositories == null) return;
            _repositories.Clear();
            _repositories = null;
        }

        private void DisposeDbContext()
        {
            _context?.Dispose();
        }
    }
}
