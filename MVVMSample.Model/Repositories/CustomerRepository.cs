using System;
using System.Threading;
using System.Threading.Tasks;
using MVVMSample.Model.Customers;
using MVVMSample.Model.Interfaces;

namespace MVVMSample.Model.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void CreateCustomer(Customer customer, Action<ResponseCode> callback)
        {
            Task<ResponseCode>.Factory.StartNew(() =>
            {
                //simulate long process:
                Thread.Sleep(TimeSpan.FromSeconds(2));

                //we don't want Hitler as a customer:
                if (string.Equals(customer.FirstName, "Adolf", StringComparison.OrdinalIgnoreCase) && 
                    string.Equals(customer.LastName, "Hitler", StringComparison.OrdinalIgnoreCase))
                    return ResponseCode.Failure;
                else
                    return ResponseCode.Success;
            }).ContinueWith(task => callback(task.Result)); //invoke callback with task result after task completes
        }
    }
}