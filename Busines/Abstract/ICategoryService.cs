using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Busines.Abstract
{
    public interface ICategoryService
    {
        Category Add(Category category);
        Category Delete(int categoryId);
        List<Category> GetList();
        Category GetByCategoryId(int categoryId);

    }
}
