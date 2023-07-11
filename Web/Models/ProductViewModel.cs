using DatabaseContext.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Web.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<EntryItems> EntryItems { get; set; }
        public virtual ICollection<OrderItems> OrderHasProduct { get; set; }
    }
}
