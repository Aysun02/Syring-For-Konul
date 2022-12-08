using Microsoft.AspNetCore.Mvc.Rendering;
using static Core.Constants.ProductStatus;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace WebApp.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public IPStatus Status { get; set; }
    }
}
