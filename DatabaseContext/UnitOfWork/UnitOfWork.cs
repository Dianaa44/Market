using System;
using System.Collections.Generic;
using System.Text;
using DatabaseContext.IRepos;
using DatabaseContext.Models;
using DatabaseContext.Repos;

namespace DatabaseContext.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        /*
         protected readonly MyCompanyContext _companyContext;
        public IDepartmentRepo DepartmentRepo { get; set; }

        public IEmployeeRepo EmployeeRepo { get; set; }

        public UnitOfWork(MyCompanyContext context)
        {
            _companyContext = context;
            DepartmentRepo = new DepartmentRepo(context);
            EmployeeRepo = new EmployeeRepo(context);
        }

        public void Dispose()
        {
            _companyContext.Dispose();
        }

        public int SaveChanges()
        {
            return _companyContext.SaveChanges();
        }
         
         */
        protected readonly MarketContext _marketContext;

        //private IPersonRepo _personRepo;

        private ISupplierRepo _supplierRepo;

        //private IBaseRepo<Entity> _baseRepo;

        private IProductRepo _productRepo;
        
        private IProductEntryRepo _productEntryRepo;
        
        private IEntryItemsRepo _entryItemsRepo;
        
        private IOrderRepo _orderRepo;
        
        private IOrderItemsRepo _orderItemsRepo;
        
        private ICustomerRepo _customerRepo;

        public UnitOfWork(MarketContext context)
        {
            _marketContext = context;
            _customerRepo = new CustomerRepo(context);
           
        }
        //public Customer AttachCustomer(Customer _customer)
        //{
        //    return _marketContext.Attach(_customer);

        //}

        //public IPersonRepo PersonRepo
        //{
        //    get
        //    {
        //        return _personRepo = _personRepo ?? new PersonRepo(_marketContext);
        //    }
        //}

        public ISupplierRepo SupplierRepo
        {
            get
            {
                return _supplierRepo = _supplierRepo ?? new SupplierRepo(_marketContext);
            }
        }

        public ICustomerRepo CustomerRepo
        {
            get
            {
                return _customerRepo = _customerRepo ?? new CustomerRepo(_marketContext);
            }
        }

        public IOrderItemsRepo OrderItemsRepo
        {
            get
            {
                return _orderItemsRepo = _orderItemsRepo ?? new OrderItemsRepo(_marketContext);
            }
        }

        public IOrderRepo OrderRepo
        {
            get
            {
                return _orderRepo = _orderRepo ?? new OrderRepo(_marketContext);
            }
        }

        public IProductEntryRepo ProductEntryRepo
        {
            get
            {
                return _productEntryRepo = _productEntryRepo ?? new ProductEntryRepo(_marketContext);
            }
        }

        public IProductRepo ProductRepo
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepo(_marketContext);
            }
        }

        public IEntryItemsRepo EntryItemsRepo
        {
            get
            {
                return _entryItemsRepo = _entryItemsRepo ?? new EntryItemsRepo(_marketContext);
            }
        }

        public void Dispose()
        {
            _marketContext.Dispose();
        }

        public int save()
        {
           return _marketContext.SaveChanges();
        }
    }
}
