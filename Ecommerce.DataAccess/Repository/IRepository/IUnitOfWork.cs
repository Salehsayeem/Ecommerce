using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork :IDisposable
    {
        ICategoryRepository category { get; }
        IFrequencyRepository frequency { get; }
        IServiceRepository service { get; }
        void save();
    }
}
