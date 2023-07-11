using DatabaseContext.Models;
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class OrderViewModel
    {

        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }


        public double? Discount { get; set; }
        public double? DeliveryPrice { get; set; }
        public string DeliveryAddress { get; set; }

        public DateTime CreationDate { get; set; }

        public List<int> ProductIds { get; set; }
        public List<double> Quantities { get; set; }
        public List<Product> Prods { get; set; }


    }
}
