using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeList.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(int? phoneNumber, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            //companies.Insert(0, new Company { Name = "Все", Id = 0 });
            //Companies = new SelectList(companies, "Id", "Name", company);
            SelectedPhoneNumber = phoneNumber;
            SelectedName = name;
        }
        //public SelectList Companies { get; private set; } // список компаний
        public int? SelectedPhoneNumber { get; private set; }   // введенный номер телефона
        public string SelectedName { get; private set; }    // введенное имя
    }
}