using DatabaseContext.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DatabaseContext.Models;
using Web.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Web.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : base(unitOfWork, webHostEnvironment)
        {
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.ProductRepo.GetAll();
            ViewBag.Message = "Hello From ViewBag";
            // ViewData["Message"] = "Hello From ViewData";
            return View(products);
        }

        public IActionResult GetProducts()
        {
            var products = _unitOfWork.ProductRepo.GetAll();
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = products
            });
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {

            if (ModelState.IsValid)
            {
                string filename = UploadFile(product);
                product.Image = filename;
                _unitOfWork.ProductRepo.Add(product);
                _unitOfWork.save();
                TempData["SweetAlertProduct"] = "suc";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SweetAlertProduct"] = "suc";
                return View();
            }
        }

        /*[HttpPost]
        public IActionResult Create(ProductViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var product = new Product {
                Name = vm.Name,
                Price = vm.Price,
                Quantity = vm.Quantity,
                Description = vm.Description,
                Image = stringFileName
                };
            _unitOfWork.ProductRepo.Add(product);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");

            return View();
            }*/


        public IActionResult Edit(int id)
        {

            var product = _unitOfWork.ProductRepo.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);

        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Price, Quantity ,theImage,Image")] Product product)
        {
            
            if (ModelState.IsValid)
            {

                if (product.theImage != null)
                {

                    deleteImage(product);

                     string newfilename = UploadFile(product);
                    product.Image = newfilename;
                }


                _unitOfWork.ProductRepo.Update(product);
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            else return View(product);
        }



        private void deleteImage(Product product)
        {
            if (!string.IsNullOrEmpty(product.Image))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", product.Image.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

        } 
        private string UploadFile(Product product)
        {
            string filename = null;
            if (product.theImage != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + product.theImage.FileName;
                string filepath = Path.Combine(uploadDir, filename);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    product.theImage.CopyTo(fileStream);
                }
            }
            return filename;

        }


        public IActionResult Delete(int id)
        {
            
            var product = _unitOfWork.ProductRepo.GetAll().FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _unitOfWork.ProductRepo.Get(id);
            deleteImage(product);
            _unitOfWork.ProductRepo.Delete(product);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Search(string value)
        {
            var products = string.IsNullOrEmpty(value) ? _unitOfWork.ProductRepo.GetAll()
                : _unitOfWork.ProductRepo.GetAll().Where(e => e.Name.ToLower().Contains(value.ToLower()));
            return Json(new
            {
                success = true,
                message = "All Data is back",
                data = products
            });
        }


        public IActionResult GetModal(int id)
        {
            return PartialView("_Modal", _unitOfWork.ProductRepo.Get(id));
        }
    }


}

