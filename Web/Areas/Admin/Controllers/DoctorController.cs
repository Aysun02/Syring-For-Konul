using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Doctor;
using WebApp.Services.Abstract;
using WebApp.Services.Concrete;
using WebApp.ViewModels.Doctor;
using WebApp.ViewModels.Product;
using Core.Entities;
using System.Net.Mime;

namespace WebApp.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)        {
            _doctorService = doctorService;

        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _doctorService.GetAllAsync();
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var isSucceeded = await _doctorService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var model = await _doctorService.GetUpdateModelAsync(Id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, DoctorUpdateVM model)
        {

            if (Id != model.Id) return NotFound();
            var isSucceeded = await _doctorService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _doctorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
