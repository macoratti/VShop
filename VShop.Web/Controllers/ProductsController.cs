using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService,
                                  ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var result = await _productService.GetAllProducts();

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await 
                 _categoryService.GetAllCategories(), "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productVM);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await
                                     _categoryService.GetAllCategories(), "CategoryId", "Name");
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.CategoryId = new SelectList(await
                               _categoryService.GetAllCategories(), "CategoryId", "Name");

            var result = await _productService.FindProductById(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(productVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
        {
            var result = await _productService.FindProductById(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _productService.DeleteProductById(id);

            if (!result)
                return View("Error");

            return RedirectToAction("Index");
        }

    }
}
