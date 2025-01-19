using DoNgocDucTourKitMiTest.Models;
using DoNgocDucTourKitMiTest.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;

namespace DoNgocDucTourKitMiTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.categoryList = await GetCategoryList();
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var data = await _productRepository.GetAll();
            return Json(data);

        }
        public async Task<Product> GetById(int id)
        {
            var data = await _productRepository.GetById(id);
            return data;
        }
        private async Task<IEnumerable<SelectListItem>> GetCategoryList()
        {
            var list = await _categoryRepository.GetAll();
            IEnumerable<SelectListItem> categoryList = list
                            .Select(x =>
                          new SelectListItem
                          {
                              Text = x.Name,
                              Value = x.Id.ToString(),
                          }
                        );
            return categoryList;
        }
        // Add or Update
        public async Task<IActionResult> AddUpdate(int id)
        {
            ViewBag.categoryList = await GetCategoryList();
            // Add
            if (id == 0)
            {
                ViewData["Title"] = "Create";
                return View();
            }
            // Update
            else
            {
                var Product = await _productRepository.GetById(id);
                if (Product == null)
                {
                    return NotFound();
                }
                ViewData["Title"] = "Update";
                return View(Product);
            }
        }
        // Add or Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUpdate(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productRepository.AddUpdate(product);
                if (result)
                {
                    TempData["success"] = "Product was successfullly saved";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Error";
                    return RedirectToAction("Index");
                }

            }
            TempData["error"] = "Error";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                TempData["error"] = "Error";
                return RedirectToAction("Index");
            }
            var product = await _productRepository.GetById(id);
            var result = await _productRepository.Delete(id);
            if (result)
            {
                TempData["success"] = $"Product: {product.Name} with Id={Convert.ToString(product.Id)} was successfullly deleted ";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = $"Product: {product.Name} with Id={Convert.ToString(product.Id)} was not deleted ";
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
