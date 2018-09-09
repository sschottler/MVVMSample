using System;
using MVVMSample.Model.Customers;

namespace MVVMSample.Model.Interfaces
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer, Action<ResponseCode> callback);
    }
}