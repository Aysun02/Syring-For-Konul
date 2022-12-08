using Core.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Services.Abstract;
using WebApp.ViewModels.Doctor;
using WebApp.ViewModels.WhatWeDoBest;
namespace Web.Areas.Admin.Services.Concrete
{
    public class WhatWeDoBestService : IWhatWeDoBestService
    {
        private readonly IWhatWeDoBestRepository _whatWeDoBestRepository;
        private readonly ModelStateDictionary _modelState;

        public WhatWeDoBestService(IWhatWeDoBestRepository whatWeDoBestRepository, IActionContextAccessor actionContextAccessor)
        {
            _whatWeDoBestRepository = whatWeDoBestRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(WhatWeDoBestCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _whatWeDoBestRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Name", "This component already is exist");
                return false;
            }


            var component = new WhatWeDoBest
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now
            };
            await _whatWeDoBestRepository.CreateAsync(component);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var component = await _whatWeDoBestRepository.GetAsync(id);
            if (component != null)
            {
                await _whatWeDoBestRepository.DeleteAsync(component);
            }
        }

        public async Task<WhatWeDoBestIndexVM> GetAllAsync()
        {

            var model = new WhatWeDoBestIndexVM
            {
                WhatWeDoBests = await _whatWeDoBestRepository.GetAllAsync()
            };

            return model;
        }

    public async Task<bool> UpdateAsync(WhatWeDoBestUpdateVM model)
    {
        var isExist = await _whatWeDoBestRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
        if (isExist)
        {
            _modelState.AddModelError("Title", "This component already exist!");
            return false;
        }
        var component = await _whatWeDoBestRepository.GetAsync(model.Id);
        if (component == null) return false;

        component.Title = model.Title;
        component.Description = model.Description;
        component.ModifiedAt = DateTime.Now;
        await _whatWeDoBestRepository.UpdateAsync(component);

        return true;
    }
    public async Task<WhatWeDoBestUpdateVM> GetUpdateModelAsync(int id)
    {
        var component = await _whatWeDoBestRepository.GetAsync(id);

        if (component != null)
        {

            var model = new WhatWeDoBestUpdateVM
            {
                Title = component.Title,
                Description = component.Description,
            };
            return model;

        }
        return null;
    }
    }

}
