﻿using Payment.EntityLayer.Concrete;

namespace Payment.BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        string TCategoryIsActiveChange(int id);
        string TCategoryIsActiveChangeCancel(int id);
    }
}
