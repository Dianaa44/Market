using DatabaseContext.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseContext.UnitOfWork
{
    public interface IUnitOfWork
    {
        //IPersonRepo PersonRepo { get; }

        ISupplierRepo SupplierRepo { get; }
        
        ICustomerRepo CustomerRepo { get; }
        
        IOrderItemsRepo OrderItemsRepo { get; }
        
        IOrderRepo OrderRepo { get; }
        
        IEntryItemsRepo EntryItemsRepo { get; } 
        
        IProductEntryRepo ProductEntryRepo { get; }
        
        IProductRepo ProductRepo { get; }
        
        public int save();

    }
}
