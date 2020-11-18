using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Extensions;
using Ecommerce.Model.ViewModels;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Http;
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
        public IActionResult AddToCart(int serviceId)
        {
            List<int> sessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(StaticDetails.SessionCart)))
            {
                sessionList.Add(serviceId);
                HttpContext.Session.SetObject(StaticDetails.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
                if (!sessionList.Contains(serviceId))
                {
                    sessionList.Add(serviceId);
                    HttpContext.Session.SetObject(StaticDetails.SessionCart, sessionList);
                }
            }
            return RedirectToAction(nameof(Index));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
