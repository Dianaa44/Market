using DatabaseContext.Models;
using DatabaseContext.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class ProductEntryController : BaseController
    {
        public ProductEntryController(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {

        }

        public IActionResult Index()
        {
            var orders = _unitOfWork.ProductEntryRepo.GetAll();

            foreach (var order in orders)
            {
                order.Supplier = _unitOfWork.SupplierRepo.Get(order.SupplierId);
            }
            ViewBag.Message = "Hello From ViewBag";
            // ViewData["Message"] = "Hello From ViewData";
            return View(orders);
        }

        public IActionResult GetProductEntries()
        {
            var productEntries = _unitOfWork.ProductEntryRepo.GetAll();
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = productEntries
            });
        }

        public IActionResult Edit(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var suppliers = _unitOfWork.SupplierRepo.GetAll();
            foreach (var supplier in suppliers)
            {
                list.Add(new SelectListItem
                {
                    Text = supplier.Name,
                    Value = supplier.Id.ToString()
                });
            }
            ViewBag.suppliers = list;
            var pEntry = _unitOfWork.ProductEntryRepo.Get(id);
            if (pEntry == null)
            {
                return NotFound();
            }
            return View(pEntry);

        }

        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,SupplierId,Date")] ProductEntry pEntry)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var suppliers = _unitOfWork.SupplierRepo.GetAll();
            foreach (var supplier in suppliers)
            {
                list.Add(new SelectListItem
                {
                    Text = supplier.Name,
                    Value = supplier.Id.ToString()
                });
            }
            ViewBag.suppliers = list;
            if (id != pEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _unitOfWork.ProductEntryRepo.Update(pEntry);
                _unitOfWork.save();


                return RedirectToAction(nameof(Index));
            }
            return View(pEntry);
        }

        public IActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var suppliers = _unitOfWork.SupplierRepo.GetAll();
            foreach (var supplier in suppliers)
            {
                list.Add(new SelectListItem
                {
                    Text = supplier.Name,
                    Value = supplier.Id.ToString()
                });
            }
            ViewBag.suppliers = list;
            return View();
        }


        [HttpPost]
        public IActionResult Create(DatabaseContext.Models.ProductEntry pEntry)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var suppliers = _unitOfWork.SupplierRepo.GetAll();
            foreach (var supplier in suppliers)
            {
                list.Add(new SelectListItem
                {
                    Text = supplier.Name,
                    Value = supplier.Id.ToString()
                });
            }
            ViewBag.suppliers = list;

            if (ModelState.IsValid)
            {
                int supID = pEntry.SupplierId;
                pEntry.Supplier = _unitOfWork.SupplierRepo.Get(supID);
                _unitOfWork.ProductEntryRepo.Add(pEntry);
                _unitOfWork.save();
                ViewBag.newname = pEntry.Supplier.Name;
                TempData["SweetAlertPE"] = "suc";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SweetAlertPE"] = "suc";
                return View();
            }
        }



        public IActionResult Delete(int id)
        {

            var pEntry = _unitOfWork.ProductEntryRepo.GetAll().FirstOrDefault(m => m.Id == id);
            if (pEntry == null)
            {
                return NotFound();
            }

            return View(pEntry);
        }

        // POST: Employees/Delete/5
        // [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var pEntry = _unitOfWork.ProductEntryRepo.Get(id);
            _unitOfWork.ProductEntryRepo.Delete(pEntry);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }


        //public IActionResult Search(string value)
        //{
        //    var deps = string.IsNullOrEmpty(value) ? _unitOfWork.ProductEntryRepo.GetAll()
        //        : _unitOfWork.ProductEntryRepo.GetAll()
        //        .Where(e => e.FirstName.ToLower().Contains(value.ToLower()) || e.LastName.ToLower().Contains(value.ToLower()));
        //    return Json(new
        //    {
        //        success = true,
        //        message = "All Data is back",
        //        data = deps
        //    });
        //}


        //public IActionResult GetModal(int id)
        //{
        //    return PartialView("_Modal",
        //        _unitOfWork.ProductEntryRepo.Get(id)
        //        );
        //}
    }
}


/*
 edit
search
 
 */