using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseContext.Models
{
    public partial class Product
    {
        public Product()
        {
            EntryItems = new HashSet<EntryItems>();
            OrderItems = new HashSet<OrderItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [Range(0,100000 , ErrorMessage = "The value must be positive.")]
        public double Price { get; set; }
        [Range(0, 100000, ErrorMessage = "The value must be positive")]
        public int Quantity { get; set; }
        public string Image { get; set; }
        [Range(0, 100000, ErrorMessage = "The value must be positive")]
        public int? AlertQuantity { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public IFormFile theImage { get; set; }
        public virtual ICollection<EntryItems> EntryItems { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
