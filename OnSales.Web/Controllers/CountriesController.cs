using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OnSales.Common.Data;
using OnSales.Common.Entities;
using OnSales.Web.Repository;

using System;
using System.Threading.Tasks;

namespace OnSales.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountriesRepository _countries;
        private readonly IDepartment _department;
        private readonly ICityRepository _cityRepository;
        private readonly DataContext _context;
        public CountriesController(DataContext dataContext, ICountriesRepository countriesRepository, 
                                   IDepartment departmentRepository,
                                   ICityRepository cityRepository)
        {
            _countries = countriesRepository;
            _department = departmentRepository;
            _context = dataContext;
            _cityRepository = cityRepository;

        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _countries.ListCountries().ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countries.GetAllDepartmentAndCities((int)id);
            //    _context.Countries
            //.Include(c => c.Departments)
            //.ThenInclude(d => d.Cities)
            //.FirstOrDefaultAsync(m => m.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _countries.CreateAsync(country);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(country);
        }


        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countries.FindByIdAsync((int)id);
                //_context.Countries
            //.Include(c => c.Departments)
            //.ThenInclude(d => d.Cities)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _countries.UpdateAsync(country);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(country);
        }
        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Country country = await _countries.GetByIdAsync((int)id);

            if (country == null)
            {
                return NotFound();
            }

            var pais = await _countries.GetByIdAsync((int)id);

            await _countries.DeleteAsync(pais);
            return RedirectToAction("Index", "Countries");
        }

        //// POST: Countries/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var country = await _countries.GetByIdAsync((int)id);

        //    await _countries.DeleteAsync(country);
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> AddDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country country = await _countries.GetByIdAsync((int)id);
            if (country == null)
            {
                return NotFound();
            }

            Department model = new Department { IdCountry = country.Id };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            //Todo arreglar
            if (ModelState.IsValid)
            {
                Country country = await _countries.GetCountryDepartment(department);
                if (country == null)
                {
                    return NotFound();
                }
                try
                {
                    department.Id = 0;
                    country.Departments.Add(department);
                    await _countries.UpdateAsync(country);
                    return RedirectToAction("Details","Countries", new { country.Id });
                    //return RedirectToAction($"{nameof(Details)}/{country.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(department);
        }
        public async Task<IActionResult> EditDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await _department.FindByIdAsync((int)id);
            if (department == null)
            {
                return NotFound();
            }

            Country country = await _countries.GetCountryDepartment(department);
            department.IdCountry = country.Id;
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _department.UpdateAsync(department);
                    return RedirectToAction("Details",  new { department.IdCountry });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(department);
        }
        public async Task<IActionResult> DeleteDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Department department = await _department.GetDepartmentCities((int)id);
            if (department == null)
            {
                return NotFound();
            }
            Country country = await _countries.GetCountryDepartmentNotNull(department);
            //country.Id = department.IdCountry;
            await _department.DeleteAsync(department);
            //await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { country.Id });
        }
        public async Task<IActionResult> DetailsDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department department = await _department.GetDepartmentCities((int)id);
            
            if (department == null)
            {
                return NotFound();
            }

            Country country = await _countries.GetCountryDepartmentNotNull(department);
        //        _context.Countries.FirstOrDefaultAsync(c =>
        //c.Departments.FirstOrDefault(d => d.Id == department.Id) != null);
            department.IdCountry = country.Id;
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department department = await _department.FindByIdAsync((int)id);
            if (department == null)
            {
                return NotFound();
            }
            City model = new City { IdDepartment = department.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(City city)
        {
            if (ModelState.IsValid)
            {
                Department department = await _department.GetDeparmentByCityAsync(city);
                   
                if (department == null)
                {
                    return NotFound();
                }
                try
                {
                    city.Id = 0;
                    department.Cities.Add(city);
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("DetailsDepartment", new {department.Id});
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(city);

        }
        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            City city = await _cityRepository.FindByIdAsync((int)id);
               // _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
           Department department = await _department.GetDepartmentNotNull((int)id,city);
            city.IdDepartment = department.Id;
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(City city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(city);

                    await _cityRepository.UpdateAsync(city);
                        //_context.SaveChangesAsync();
                    return RedirectToAction("DetailsDepartment", new{city.IdDepartment});
                }catch(DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch(Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(city);
        }
        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            City city = await _context.Cities
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            Department department = await _department.GetDepartmentNotNull((int)id,city);
                //_context.Departments.FirstOrDefaultAsync(d =>d.Cities.FirstOrDefault(c => c.Id == city.Id) != null);
            //_context.Cities.Remove(city);
            await _cityRepository.DeleteAsync(city);  
            return RedirectToAction("DetailsDepartment", new {department.Id});
        }
    }
}
