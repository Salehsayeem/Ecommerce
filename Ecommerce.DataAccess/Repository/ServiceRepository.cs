using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.DataAccess.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

    

        public void Update(Service service)
        {
            var obj = _db.Service.FirstOrDefault(p => p.Id == service.Id);
            obj.Name = service.Name;
            obj.Description = service.Description;
            obj.Price = service.Price;
            obj.ImageUrl = service.ImageUrl;
            obj.FrequencyId = service.FrequencyId;
            obj.CategoryId = service.CategoryId;
            _db.SaveChanges();
        }
    }
}
