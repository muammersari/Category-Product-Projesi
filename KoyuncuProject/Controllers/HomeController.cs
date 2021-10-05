using Busines.Abstract;
using Entities.Concrete;
using Entities.Dto.ViewModel;
using KoyuncuProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KoyuncuProject.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var result = _productService.GetList();
            ViewModel vm = new ViewModel();
            vm.products = result;
            return View(result);
        }

        public IActionResult Home()
        {
            var product = _productService.GetList();
            var category = _categoryService.GetList();
            ViewModel vm = new ViewModel();
            vm.products = product;
            vm.categories = category;
            return View(vm);
        }

        public JsonResult CategoryGetlist()
        {
            var result = _categoryService.GetList();
            if (result == null)
            {
                return Json(false);
            }
            return Json(result);
        }
        public JsonResult CategoryAdd(Category category)
        {
            var result = _categoryService.Add(category);
            if (result == null)
            {
                return Json(false);
            }
            return Json(result);
        }

        public JsonResult ProductAdd(Product product)
        {
            var result = _productService.Add(product);
            if (result == null)
            {
                return Json(false);
            }
            var category = _categoryService.GetByCategoryId(result.CategoryId);
            result.Category = category;
            return Json(result);
        } 

        [HttpPost]
        public JsonResult ProductDelete(int prodcutId)
        {
            var result = _productService.Delete(prodcutId);
            if (result == null)
            {
                return Json(false);
            }
            return Json(true);
        }

    }
}
