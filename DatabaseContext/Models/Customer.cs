using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseContext.Models
{
    public partial class Customer
    {
        public int Id { get; set; }

        public bool IsVip { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        [StringLength(50, ErrorMessage = "You have exceeded the Maximum number Of Charechters!")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "This Field is required")]
        [StringLength(50, ErrorMessage = "You have exceeded the Maximum number Of Charechters!")]
        public string LastName { get; set; }

        // public virtual Person IdNavigation { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
/**
 
 public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }

        
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
 
 */