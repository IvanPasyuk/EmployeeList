using EmployeeList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }

        public async Task<IActionResult> Index(int phonenumber, string name)
        {
            IQueryable<Employee> employees = db.Employees.Include(p => p.PhoneNumber);
            if (phonenumber != 0 && phonenumber > 0)
            {
                employees = employees.Where(p => p.PhoneNumber == phonenumber);
            }

            if (!String.IsNullOrEmpty(name))
            {
                employees = employees.Where(p => p.Name.Contains(name));
            }

            //db.Employees = employees as DbSet<Employee>;                     

            return View(await db.Employees.ToListAsync());
        }

        //public async Task<IActionResult> Index(int? phonenumber, string name, int page = 1)
        //{
        //    int pageSize = 3;

        //    //фильтрация
        //    IQueryable<Employee> employees = db.Employees.Include(x => x != null);

        //    if (phonenumber != null && phonenumber != 0 && phonenumber > 0)
        //    {
        //        employees = employees.Where(p => p.PhoneNumber == phonenumber);
        //    }
        //    if (!String.IsNullOrEmpty(name))
        //    {
        //        employees = employees.Where(p => p.Name.Contains(name));
        //    }

        //    //// сортировка
        //    //switch (sortOrder)
        //    //{
        //    //    case SortState.NameDesc:
        //    //        users = users.OrderByDescending(s => s.Name);
        //    //        break;
        //    //    case SortState.AgeAsc:
        //    //        users = users.OrderBy(s => s.Age);
        //    //        break;
        //    //    case SortState.AgeDesc:
        //    //        users = users.OrderByDescending(s => s.Age);
        //    //        break;
        //    //    case SortState.CompanyAsc:
        //    //        users = users.OrderBy(s => s.Company.Name);
        //    //        break;
        //    //    case SortState.CompanyDesc:
        //    //        users = users.OrderByDescending(s => s.Company.Name);
        //    //        break;
        //    //    default:
        //    //        users = users.OrderBy(s => s.Name);
        //    //        break;
        //    //}

        //    // пагинация
        //    var count = await employees.CountAsync();
        //    var items = await employees.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        //    // формируем модель представления
        //    IndexViewModel viewModel = new IndexViewModel
        //    {
        //        PageViewModel = new PageViewModel(count, page, pageSize),
        //        //SortViewModel = new SortViewModel(sortOrder),
        //        FilterViewModel = new FilterViewModel(phonenumber, name),
        //        Employees = items
        //    };
        //    return View(viewModel);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Employee employee = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            db.Employees.Update(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Employee employee = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Employee employee = new Employee { Id = id.Value };
                db.Entry(employee).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
