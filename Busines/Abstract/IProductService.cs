using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Busines.Abstract
{
    public interface IProductService
    {
        Product Add(Product product);
        Product Delete(int productId);
        List<Product> GetList();
        List<Product> GetByCategoryId(int categoryId);
    }
}
