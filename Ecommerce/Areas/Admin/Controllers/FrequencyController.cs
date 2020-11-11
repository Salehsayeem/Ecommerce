using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FrequencyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Frequency frequency = new Frequency();
            if (id == null)
            {
                return View(frequency);
            }
            frequency = _unitOfWork.frequency.Get(id.GetValueOrDefault());
            if (frequency == null)
            {
                return NotFound();
            }
            return View(frequency);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Frequency frequency)
        {
            if (ModelState.IsValid)
            {
                if (frequency.Id == 0)
                {
                    _unitOfWork.frequency.Add(frequency);
                }
                else
                {
                    _unitOfWork.frequency.Update(frequency);
                }
                _unitOfWork.save();
                return RedirectToAction(nameof(Index));
            }
            return View(frequency);
        }

        #region API calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.frequency.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.frequency.Get(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }
            _unitOfWork.frequency.Remove(obj);
            _unitOfWork.save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
