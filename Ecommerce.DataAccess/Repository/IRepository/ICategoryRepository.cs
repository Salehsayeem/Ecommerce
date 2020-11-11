using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DataAccess.Repository.IRepository
{
   public interface ICategoryRepository : IRepository<Category> 
    {
        IEnumerable<SelectListItem> GetCategoryListForDropdown();
        void Update(Category category);
    }
}
