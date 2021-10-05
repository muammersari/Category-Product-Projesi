using Busines.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Busines.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product Add(Product product)
        {
            if (_productDal.Get(x => x.ProductName == product.ProductName) != null) // eğer aynı isimde başka bir ürün varsa ürün eklenemiyor
            {
                return null;
            }
            var result = _productDal.Add(product);
            if (result == null) // ürün eklenirken bir problkem oluşursa ürün eklenemiyor
            {
                return null;
            }
            return product;
        }

        public Product Delete(int productId)
        {
            var result = _productDal.Get(x => x.ProductId == productId);
            if (result == null)
            {
                return null;
            }
            _productDal.Delete(result);
            return result;
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            var result = _productDal.GetList(x => x.CategoryId == categoryId).ToList();
            if (result.Count == 0) // ürün listesi yoksa null döndürülüyor
            {
                return null;
            }
            return result;
        }
        public List<Product> GetList()
        {
            var result = _productDal.GetList().ToList(); // tüm ürünbler listeleniyor
            if (result.Count == 0) // ürün listesi yoksa null döndürülüyor
            {
                return null;
            }
            return result;
        }


    }
}

