using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto.ViewModel
{
    public class ViewModel :IDto
    {
        public IList<Product> products { get; set; }
        public IList<Category> categories { get; set; }
    }
}
