using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Abstract;
using WebApp.Services.Concrete;
using WebApp.ViewModels.Doctor;
using WebApp.ViewModels.WhatWeDoBest;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WhatWeDoBestController : Controller
    {
        private readonly IWhatWeDoBestService _whatWeDoBestService;

        public WhatWeDoBestController(IWhatWeDoBestService whatWeDoBestService)
        {
            _whatWeDoBestService = whatWeDoBestService;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _whatWeDoBestService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WhatWeDoBestCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var isSucceeded = await _whatWeDoBestService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var model = await _whatWeDoBestService.GetUpdateModelAsync(Id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, WhatWeDoBestUpdateVM model)
        {

            if (Id != model.Id) return NotFound();
            var isSucceeded = await _whatWeDoBestService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _whatWeDoBestService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
