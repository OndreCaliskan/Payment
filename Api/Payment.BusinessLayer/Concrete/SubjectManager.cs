using Payment.BusinessLayer.Abstract;
using Payment.DataAccessLayer.Abstract;
using Payment.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.BusinessLayer.Concrete
{
    public class SubjectManager : ISubjectService
    {
        private readonly ISubjectDal _subjectDal;

        public SubjectManager(ISubjectDal subjectDal)
        {
            _subjectDal = subjectDal;
        }

        public void TDelete(Subject t)
        {
            
            _subjectDal.Delete(t);
        }

        public Subject TGetByID(int id)
        {
           return _subjectDal.GetByID(id);
        }

        public List<Subject> TGetList()
        {
            return _subjectDal.GetList();
        }

        public void TInsert(Subject t)
        {
            _subjectDal.Insert(t);  
        }

        public void TUpdate(Subject t)
        {
           _subjectDal.Update(t);
        }
    }
}
