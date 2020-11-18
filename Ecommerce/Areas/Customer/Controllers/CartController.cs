using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Extensions;
using Ecommerce.Model;
using Ecommerce.Model.ViewModels;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartViewModel CartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartVM = new CartViewModel()
            {
                OrderHeader = new Model.OrderHeader(),
                ServiceList = new List<Service>()
            };
        }
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
                foreach(int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfWork.service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }
            return View(CartVM);
        }
    }
}
