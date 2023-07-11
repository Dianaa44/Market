using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseContext.Models
{

    public class ApplicationUser : IdentityUser<string>
    {
        public virtual Customer Customer { get; set; }
    }
}
