using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseContext.Models
{
    public partial class Supplier
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public string Name { get; set; }

        //public virtual Person IdNavigation { get; set; }
        public virtual ICollection<ProductEntry> ProductEntry { get; set; }
    }
}
