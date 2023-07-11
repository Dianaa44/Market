using DatabaseContext.Models;
using DatabaseContext.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class SupplierController : BaseController
    {
        public SupplierController(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {

        }

        public IActionResult Index()
        {
            var suppls = _unitOfWork.SupplierRepo.GetAll();
            ViewBag.Message = "Hello From ViewBag";
            return View(suppls);
        }

        public IActionResult GetCustomers()
        {
            var suppls = _unitOfWork.SupplierRepo.GetAll();
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = suppls
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
           
                var supplier = _unitOfWork.SupplierRepo.Get(id);
                if (supplier == null)
                {
                    return NotFound();
                }
                return View(supplier);
           
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                    _unitOfWork.SupplierRepo.Update(supplier);
                    _unitOfWork.save();
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }


        [HttpPost]
        public IActionResult Create(DatabaseContext.Models.Supplier
            supplier)
        {

            if (ModelState.IsValid)
            {

                // ViewBag.Message = "Name must be required";
                _unitOfWork.SupplierRepo.Add(supplier);
                _unitOfWork.save();
                TempData["SweetAlertSup"] = "suc";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SweetAlertSup"] = "suc";
                return View();
            }
        }


        public  IActionResult Delete(int id)
        {

            var supplier = _unitOfWork.SupplierRepo.GetAll().FirstOrDefault(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Employees/Delete/5
        // [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public  IActionResult DeleteConfirmed(int id)
        {
            var supplier =  _unitOfWork.SupplierRepo.Get(id);
            _unitOfWork.SupplierRepo.Delete(supplier);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Search(string value)
        {
            var suppls = string.IsNullOrEmpty(value) ? _unitOfWork.SupplierRepo.GetAll()
                : _unitOfWork.SupplierRepo.GetAll()
                .Where(e => e.Name.ToLower().Contains(value.ToLower()));
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = suppls
            });
        }


        public IActionResult GetModal(int id)
        {
            return PartialView("_Modal",
                _unitOfWork.SupplierRepo.Get(id)
                );
        }
    }
}
