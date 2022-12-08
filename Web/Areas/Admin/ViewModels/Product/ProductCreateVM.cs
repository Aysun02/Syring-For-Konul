using Core.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using static Core.Constants.ProductStatus;

namespace WebApp.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public ProductStatus Status { get; set; }

    }
}
