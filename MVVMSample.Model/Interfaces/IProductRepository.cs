using System;
using MVVMSample.Model.Products;

namespace MVVMSample.Model.Interfaces
{
    public interface IProductRepository
    {
        void CreateProduct(Product product, Action<ResponseCode> callback);
    }
}