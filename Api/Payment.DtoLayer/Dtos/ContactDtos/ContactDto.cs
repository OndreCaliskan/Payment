
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.DtoLayer.Dtos.ContactDtos
{
    public class ContactDto
    {
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string SubjectName { get; set; }
        public int SubjectID { get; set; }
        public string Message { get; set; }
    }
}
