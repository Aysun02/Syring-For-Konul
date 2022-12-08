using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.ViewModels.Doctor
{
    public class DoctorUpdateVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Qualification { get; set; }
    }
}
