using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Model;
using Ecommerce.Model.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public ServiceVM serviceVM { get; set; }
        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            serviceVM = new ServiceVM()
            {
                service = new Service(),
                CategoryList = _unitOfWork.category.GetCategoryListForDropdown(),
                FrequencyList = _unitOfWork.frequency.GetFrequencyListForDropdown()
            };
            if (id != null)
            {
                serviceVM.service = _unitOfWork.service.Get(id.GetValueOrDefault());
            }
            return View(serviceVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (serviceVM.service.Id == 0)
                {
                    //New Service
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\services");
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    serviceVM.service.ImageUrl = @"images\services\" + fileName + extension;
                    _unitOfWork.service.Add(serviceVM.service);
                }
                else
                {
                    //Edit Service
                    var serviceFromDb = _unitOfWork.service.Get(serviceVM.service.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        serviceVM.service.ImageUrl = @"\images\services\" + fileName + extension_new;
                    }
                    else
                    {
                        serviceVM.service.ImageUrl = serviceFromDb.ImageUrl;
                    }

                    _unitOfWork.service.Update(serviceVM.service);
                }
                _unitOfWork.save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                serviceVM.CategoryList = _unitOfWork.category.GetCategoryListForDropdown();
                serviceVM.FrequencyList = _unitOfWork.frequency.GetFrequencyListForDropdown();
                return View(serviceVM);
            }
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.service.GetAll(includeProperties: "Category,Frequency") });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.service.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (serviceFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _unitOfWork.service.Remove(serviceFromDb);
            _unitOfWork.save();
            return Json(new { success = true, message = "Deleted Successfully." });
        }

        #endregion
    }
}
