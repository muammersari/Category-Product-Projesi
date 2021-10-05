using Busines.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Busines.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public Category Add(Category category)
        {
            if (_categoryDal.Get(x => x.CategoryName == category.CategoryName) != null) // aynı isimde kategori ekli ise kategori eklenmiyor
            {
                return null;
            }
            var result = _categoryDal.Add(category);
            if (result == null) // kategori eklenirken bir problkem oluşursa kategori eklenemiyor
            {
                return null;
            }
            return category;
        }

        public Category Delete(int categoryId)
        {
            var result = _categoryDal.Get(x => x.CategoryId == categoryId);
            if (result == null)
            {
                return null;
            }
            _categoryDal.Delete(result);
            return result;
        }

        public Category GetByCategoryId(int categoryId)
        {
            var result = _categoryDal.Get(x => x.CategoryId == categoryId);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public List<Category> GetList()
        {
            var result = _categoryDal.GetList().ToList(); //tüm kategoriler listeleniyor
            if (result.Count == 0)
            {
                return null;
            }
            return result;
        }
    }
}
