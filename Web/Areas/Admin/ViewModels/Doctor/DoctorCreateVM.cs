using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.ViewModels.Doctor
{
    public class DoctorCreateVM
    {
        [Required]
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Qualification { get; set; }
    }
}
