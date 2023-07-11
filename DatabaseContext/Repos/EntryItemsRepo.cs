using DatabaseContext.IRepos;
using DatabaseContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseContext.Repos
{
    public class EntryItemsRepo :BaseRepo<EntryItems>, IEntryItemsRepo
    {

        public EntryItemsRepo(MarketContext context) : base(context)// prametrized constructor that take some context and start making changes
        {
           
        }

        
    }
}
