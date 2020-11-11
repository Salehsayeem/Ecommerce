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
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        private readonly ApplicationDbContext _db;
        public FrequencyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetFrequencyListForDropdown()
        {
            return _db.Frequency.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

        }

        public void Update(Frequency frequency)
        {
            var obj = _db.Frequency.FirstOrDefault(p => p.Id == frequency.Id);
            obj.Name = frequency.Name;
            obj.FrequencyCount = frequency.FrequencyCount;
            _db.SaveChanges();
        }
    }
}
