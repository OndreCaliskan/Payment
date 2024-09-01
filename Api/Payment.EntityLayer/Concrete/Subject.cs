using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.EntityLayer.Concrete
{
    public class Subject
    {
        
        public int SubjectID { get; set; }
        public string? Description { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
