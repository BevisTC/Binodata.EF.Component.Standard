using System;
using System.Linq;
using System.Linq.Expressions;

namespace Binodata.EF.Component.Standard.Repository
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="entity"></param>
        void Edit(T entity);

        /// <summary>
        /// do save changes
        /// </summary>
        /// <returns></returns>
         int Save();

        /// <summary>
        /// 查詢資料
        /// </summary>
        /// <param name="predicate">傳入Lamda 語法</param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}