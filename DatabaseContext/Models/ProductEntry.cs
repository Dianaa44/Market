using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseContext.Models
{
    public partial class ProductEntry
    {
        public ProductEntry()
        {
            EntryItems = new HashSet<EntryItems>();
        }

        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime? Date { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<EntryItems> EntryItems { get; set; }
    }
}
