using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseContext.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public double TotalPrice { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        [Range(0, 100000, ErrorMessage = "The value must be positive")]


        public double? Discount { get; set; }
        public double? DeliveryPrice { get; set; }
        public string DeliveryAddress { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }


        [NotMapped]
        public List<int> ProductIds { get; set;}
        [NotMapped]
        public List<Product> Products { get; set; }
        [NotMapped]
        public List<double> Quantities { get; set;}

        public virtual Customer customer { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }

        public double CalculateTotalPrice()
        {
            double total = 0;
            for (var i =0 ; i < ProductIds.Count; i++)
            {
                total += ProductIds[i] * Quantities[i];
            }
            return (double)(total * Discount);
        }
    }
}
