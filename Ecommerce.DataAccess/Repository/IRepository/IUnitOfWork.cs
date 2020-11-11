using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork :IDisposable
    {
        ICategoryRepository category { get; }
        void save();
    }
}
