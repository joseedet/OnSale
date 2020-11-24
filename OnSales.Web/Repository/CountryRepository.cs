using Microsoft.EntityFrameworkCore;

using OnSales.Common.Data;
using OnSales.Common.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSales.Web.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext dataContext):base(dataContext)
        {
            _context = dataContext;
        }

        
        public  Task<Country> GetAllDepartmentAndCities(int id)
        {
           return _context.Countries
            .Include(c => c.Departments)
            .ThenInclude(d => d.Cities)
            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Country> GetDepartmentCountry(Department department)
        {
            return (IQueryable<Country>)_context.Countries
            .Include(c => c.Departments).FirstOrDefault(c=> c.Id==department.Id); 
        }
        public async Task<Country> GetCountryDepartment(Department department)
        {

            return await _context.Countries
            .Include(c => c.Departments).FirstOrDefaultAsync(c => c.Id == department.Id);
        }

        public  IQueryable<Country> ListCountries()
        {
            return _context.Countries
            .Include(c => c.Departments);
        }
    }
}
