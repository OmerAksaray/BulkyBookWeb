using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
           List<Product> products= _unitOfWork.Product.GetAll().ToList();
            
            return View(products);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"]=CategoryList;
            ProductVM productVM= new()
            {
                CategoryList= CategoryList,
                Product=new Product()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM productVM
            )
        {
            if (ModelState.IsValid)
            { 
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Producct created succesffuly";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(productVM);
            }
               
        }
        public IActionResult Edit(int? id)
        {
          Product productFromDb =  _unitOfWork.Product.Get(n=>n.Id == id);

            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid) { 
            _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated succesffuly";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            Product productFromDb = _unitOfWork.Product.Get(n => n.Id == id);
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Delete(Product obj)
        {
            _unitOfWork.Product.Remove(obj);    
            _unitOfWork.Save();
            TempData["success"] = "Product deleted succesffuly";
            return RedirectToAction("Index", "Product");
        }
    }
}
