using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Model.ViewModels;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private HomeViewModel homeVm;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            homeVm = new HomeViewModel()
            {
                CategoryList = _unitOfWork.category.GetAll(),
                ServiceList = _unitOfWork.service.GetAll(includeProperties: "Frequency")

            };
            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            var serviceFromDb = _unitOfWork.service.GetFirstOrDefault(includeProperties: "Category,Frequency",filter:x=>x.Id==id);
            return View(serviceFromDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
