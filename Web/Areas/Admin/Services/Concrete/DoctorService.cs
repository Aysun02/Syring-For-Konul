using Core.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.Doctor;
using WebApp.Services.Abstract;


namespace WebApp.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ModelStateDictionary _modelState;

        public DoctorService(IDoctorRepository doctorRepository, IActionContextAccessor actionContextAccessor)
        {
            _doctorRepository = doctorRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region Index
        public async Task<DoctorIndexVM> GetAllAsync()
        {
            var model = new DoctorIndexVM
            {
                Doctors = await _doctorRepository.GetAllAsync()
            };
            
            return model;
        }
        #endregion 

        #region Create
        public async Task<bool> CreateAsync(DoctorCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _doctorRepository.AnyAsync(c => c.FullName.Trim().ToLower() == model.FullName.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Name", "This Doctor already is exist");
                return false;
            }


            var doctor = new Doctor
            {
                FullName = model.FullName,
                Description = model.Description,
                Qualification = model.Qualification,
                CreatedAt = DateTime.Now
            };
            await _doctorRepository.CreateAsync(doctor);
            return true;
        }
        #endregion

        #region Update
        public async Task<DoctorUpdateVM> GetUpdateModelAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);

            if (doctor != null)
            {

                var model = new DoctorUpdateVM
                {
                    FullName = doctor.FullName,
                    Description = doctor.Description,
                    Qualification = doctor.Qualification
                };
                return model;

            }
            return null;
        }

        public async Task<bool> UpdateAsync(DoctorUpdateVM model)
        {
            var isExist = await _doctorRepository.AnyAsync(c => c.FullName.Trim().ToLower() == model.FullName.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Name", "This Doctor already Work with us!");
                return false;
            }
            var doctor = await _doctorRepository.GetAsync(model.Id);
            if (doctor == null) return false;

            doctor.FullName = model.FullName;
            doctor.Description = model.Description;
            doctor.Qualification = model.Qualification;
            doctor.ModifiedAt = DateTime.Now;
            await _doctorRepository.UpdateAsync(doctor);

            return true;
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            if (doctor != null)
            {
                await _doctorRepository.DeleteAsync(doctor);
            }
        }
        #endregion

    }
}
