using DatabaseContext.Models;
using DatabaseContext.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {

        }

        public IActionResult Index(int pg = 1)
        {
            var customers = _unitOfWork.CustomerRepo.GetAll();
            var sortedCustomers = customers.OrderByDescending(x => x.Id).ToList();

            const int pageSize = 4;
            if (pg < 1)
                pg = 1;

            int resCount = sortedCustomers.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = sortedCustomers.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
             return View(data);

            //return View(customers);
        }

        public IActionResult GetCustomers()
        {
            var deps = _unitOfWork.CustomerRepo.GetAll();
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = deps
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
           
                var customer = _unitOfWork.CustomerRepo.Get(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
           
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,IsVip")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                    _unitOfWork.CustomerRepo.Update(customer);
                    _unitOfWork.save();
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        [HttpPost]
        public IActionResult Create(DatabaseContext.Models.Customer
            customer)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CustomerRepo.Add(customer);
                _unitOfWork.save();
                TempData["SweetAlert"] = "suc";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SweetAlert"] = "oop";
                return View();
            }
        }


        public  IActionResult Delete(int id)
        {

            var customer = _unitOfWork.CustomerRepo.GetAll().FirstOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Employees/Delete/5
        // [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public  IActionResult DeleteConfirmed(int id)
        {
            var customer =  _unitOfWork.CustomerRepo.Get(id);
            _unitOfWork.CustomerRepo.Delete(customer);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Search(string value)
        {
            var deps = string.IsNullOrEmpty(value) ? _unitOfWork.CustomerRepo.GetAll()
                : _unitOfWork.CustomerRepo.GetAll()
                .Where(e => e.FirstName.ToLower().Contains(value.ToLower()) || e.LastName.ToLower().Contains(value.ToLower()));
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = deps
            });
        }


        [HttpGet]
        public IActionResult GetCustomer(int customerId)
        {
            var customer = _unitOfWork.CustomerRepo.Get(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Json(new
            {
                f_name = customer.FirstName,
                l_name = customer.LastName,
                is_vip = customer.IsVip
            });
        }
        
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _unitOfWork.CustomerRepo.GetAll();

            if (customers == null)
            {
                return NotFound();
            }

            var customerList = new List<Object>();
            foreach (var customer in customers)
            {
                customerList.Add(new
                {
                    f_name = customer.FirstName,
                    l_name = customer.LastName,
                    is_vip = customer.IsVip
                });
            }

            return Json(customerList);
        }
    }

}
