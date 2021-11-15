using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeList.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } // ФИО сотрудника

        public string Department { get; set; } // Название отдела

        public int PhoneNumber { get; set; } // Номер телефона сотрудника

        public string Photo { get; set; } //Фотография сотрудника        
    }
}
