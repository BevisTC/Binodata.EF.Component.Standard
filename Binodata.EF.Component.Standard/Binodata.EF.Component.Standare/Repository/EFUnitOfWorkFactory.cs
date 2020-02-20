using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Binodata.EF.Component.Standard.Repository
{
    public class EFUnitOfWorkFactory
    {
        /// <summary>
        /// Get Entity framework unit of work
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static IUnitOfWork GetUnitOfWork<TContext>() where TContext : DbContext, new()
        {
            return new EFUnitOfWork(new TContext());
        }
    }
}
