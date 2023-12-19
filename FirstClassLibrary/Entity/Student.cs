using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstClassLibrary.Entity
{
    public class Student
    {

        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public int Age { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
