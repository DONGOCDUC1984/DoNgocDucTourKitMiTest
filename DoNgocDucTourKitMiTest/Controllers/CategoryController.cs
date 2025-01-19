
using Microsoft.AspNetCore.Mvc;

namespace DoNgocDucTourKitMiTest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var data = await _categoryRepository.GetAll();
            return Json(data);

        }
        public async Task<Category> GetById(int id)
        {
            var data = await _categoryRepository.GetById(id);
            return data;
        }
        // Add or Update
        public async Task<IActionResult> AddUpdate(int id)
        {
            // Add
            if (id == 0)
            {
                ViewData["Title"] = "Create";
                return View();
            }
            // Update
            else
            {
                var category = await _categoryRepository.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                ViewData["Title"] = "Update";
                return View(category);
            }
        }
        // Add or Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUpdate(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryRepository.AddUpdate(category);
                if (result)
                {
                    TempData["success"] = "Category was successfullly saved";
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
            var category = await _categoryRepository.GetById(id);
            var result = await _categoryRepository.Delete(id);
            if (result)
            {
                TempData["success"] = $"Category: {category.Name} with Id={Convert.ToString(category.Id)} was successfullly deleted ";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = $"Category: {category.Name} with Id={Convert.ToString(category.Id)} was not deleted ";
                return RedirectToAction("Index");
            }
        }

    }
}
