using DatabaseContext.IRepos;
using DatabaseContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseContext.Repos
{
    public class ProductRepo :BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(MarketContext context) :base (context)
        {
            
        }

       
    }
}
