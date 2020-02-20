using System;
using System.Collections.Generic;
using System.Text;
using Binodata.EF.Component.Standard.Repository;

namespace Binodata.EF.Component.Standard
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IGenericRepository<T> GetGenericRepository<T>() where T : class;

        /// <summary>
        /// 最後完成 Commit
        /// </summary>
        int Save();
    }
}
