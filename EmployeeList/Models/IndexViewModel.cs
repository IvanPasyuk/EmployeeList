using System.Collections.Generic;

namespace EmployeeList.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}