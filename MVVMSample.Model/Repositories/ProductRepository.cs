using System;
using System.Threading;
using System.Threading.Tasks;
using MVVMSample.Model.Interfaces;
using MVVMSample.Model.Products;

namespace MVVMSample.Model.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void CreateProduct(Product product, Action<ResponseCode> callback)
        {
            Task<ResponseCode>.Factory.StartNew(() =>
            {
                //simulate long process:
                Thread.Sleep(TimeSpan.FromSeconds(2));

                return ResponseCode.Success;
            }).ContinueWith(task => callback(task.Result)); //invoke callback with task result after task completes
        }
    }
}