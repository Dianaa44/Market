using DatabaseContext.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DatabaseContext.Models;
using Web.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Web.Models;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : base(unitOfWork, webHostEnvironment)
        {
        }

        public IActionResult Index()
        {
            var orders = _unitOfWork.OrderRepo.GetAll();

            foreach (var order in orders)
            {
                order.customer = _unitOfWork.CustomerRepo.Get(order.CustomerId);
            }
            return View(orders);
        }


        public IActionResult GetOrders()
        {
            var orders = _unitOfWork.OrderRepo.GetAll();
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = orders
            });
        }
        public IActionResult Create()
        {
            var list2= _unitOfWork.ProductRepo.GetAll();
            ViewBag.products = list2;
            List<SelectListItem> list = new List<SelectListItem>();
            var customers = _unitOfWork.CustomerRepo.GetAll();
            foreach (var customer in customers)
            {
                list.Add(new SelectListItem
                {
                    Text = customer.FirstName,
                    Value = customer.Id.ToString()
                });
            }
            ViewBag.customers = list;
            return View();
        }


        [HttpPost]
     
        public IActionResult Create(Order order)
        {
            order.CreationDate = DateTime.Now;
            string listIdsJson = HttpContext.Request.Form["listIds"];
            string listQuantitiesJson = HttpContext.Request.Form["listQuantities"];

            List<int> listIds = JsonConvert.DeserializeObject<List<int>>(listIdsJson);
            List<double> listQuantities = JsonConvert.DeserializeObject<List<double>>(listQuantitiesJson);

            order.ProductIds = listIds;
            order.Quantities = listQuantities;
        

            List<SelectListItem> list = new List<SelectListItem>();
            List<Product> prodList = new List<Product>();
            var customers = _unitOfWork.CustomerRepo.GetAll();
            foreach (var customer in customers)
            {
                list.Add(new SelectListItem
                {
                    Text = customer.FirstName,
                    Value = customer.Id.ToString()
                });
            }
            ViewBag.customers = list;
            
            var list2 = _unitOfWork.ProductRepo.GetAll();
            ViewBag.products = list2;
            //order.ProductIds = listIds;
            //order.Quantities = listQuantities;
            var prods = _unitOfWork.ProductRepo.GetAll();

            if (ModelState.IsValid)
            {
                int customerID = order.CustomerId;
                order.customer = _unitOfWork.CustomerRepo.Get(customerID);
                for (var i = 0; i < listIds.Count; i++)
                {
                    //order.TotalPrice += _unitOfWork.ProductRepo.Get(listIds[i]).Price * listQuantities[i];
                _unitOfWork.ProductRepo.Get(listIds[i]).Quantity -= (int) listQuantities[i];
                    prodList.Add(_unitOfWork.ProductRepo.Get(listIds[i]));
                
                }
                order.Products = prodList;
                //order.TotalPrice = order.CalculateTotalPrice();
                _unitOfWork.OrderRepo.Add(order);
                _unitOfWork.save();

                for (var i = 0; i < listIds.Count; i++)
                {
                    OrderItems orderItems = new OrderItems();
                    orderItems.OrderId = order.Id;
                    orderItems.ProductId = listIds[i];
                    orderItems.Quantity = (int)listQuantities[i];
                    _unitOfWork.OrderItemsRepo.Add(orderItems);
                    _unitOfWork.save();
                }


                TempData["SweetAlertOrder"] = "suc";
                ViewBag.newname = order.customer.FirstName;
              
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SweetAlertOrder"] = "suc";
                return View();
            }
        }


        public IActionResult Edit(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var customers = _unitOfWork.CustomerRepo.GetAll();
            foreach (var customer in customers)
            {
                list.Add(new SelectListItem
                {
                    Text = customer.FirstName,
                    Value = customer.Id.ToString()
                });
            }
            ViewBag.customers = list;
            var order = _unitOfWork.OrderRepo.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);

        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,CustomerId,TotalPrice,Discount,Taxes, Status , Deliveryprice,Address,OrderItems")] Order order)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var customers = _unitOfWork.CustomerRepo.GetAll();
            foreach (var customer in customers)
            {
                list.Add(new SelectListItem
                {
                    Text = customer.FirstName,
                    Value = customer.Id.ToString()
                });
            }
            ViewBag.customers = list;

            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.OrderRepo.Update(order);
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            else return View(order);
        }




        public IActionResult Delete(int id)
        {

            var order = _unitOfWork.OrderRepo.GetAll().FirstOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _unitOfWork.OrderRepo.Get(id);
            _unitOfWork.OrderRepo.Delete(order);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }


         public IActionResult Details(int id)
        {
            var orderitems = _unitOfWork.OrderItemsRepo.GetAll();
            var prodList = new List<Product>();
            var quanList = new List<int>();
            foreach (var item in orderitems)
            {
                if (item.OrderId == id)
                    prodList.Add(_unitOfWork.ProductRepo.Get((int)item.ProductId));
                    quanList.Add((int)item.Quantity);
            }
            ViewBag.prods = prodList;
            ViewBag.quans = quanList;

            var order = _unitOfWork.OrderRepo.GetAll().FirstOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        /* public IActionResult Search(string value)
         {
             var orders = string.IsNullOrEmpty(value) ? _unitOfWork.OrderRepo.GetAll()
                 : _unitOfWork.OrderRepo.GetAll().Where(e => e.Name.ToLower().Contains(value.ToLower()));
             return Json(new
             {
                 success = true,
                 message = "All Data is back",
                 data = orders
             });
         }
        */

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _unitOfWork.OrderRepo.GetAll();

            if (orders == null)
            {
                return NotFound();
            }

            var orderList = new List<Object>();
            foreach (var order in orders)
            {
                orderList.Add(new
                {
                    ID = order.Id,
                    customerName = _unitOfWork.CustomerRepo.Get(order.CustomerId).FirstName,
                    CustomerId = order.CustomerId,
                    status = order.Status,
                    totalPrice = order.TotalPrice,
                    discount = order.Discount,
                    deliveryPrice = order.DeliveryPrice,
                    deliveryAddress = order.DeliveryAddress,
                    creationDate = order.CreationDate
                });
            }

            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = orderList
            });
        }



        public ActionResult GetAllordersAtDate(DateTime startDate, DateTime endDate)
        {
            // Retrieve orders from the database that fall within the specified date range
            var orders = _unitOfWork.OrderRepo.Find((o => o.CreationDate >= startDate && o.CreationDate <= endDate)).ToList();

            // Convert the orders to a JSON object and return it to the client
            return Json(new { success = true, data = orders });
        }



        //public IActionResult GetOrderDetails(int id)
        //{
        //    var order = _unitOfWork.OrderRepo.GetAll()
        //        .Include(o => o.Products) // Include the products
        //        .FirstOrDefault(o => o.Id == id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

    }
}

