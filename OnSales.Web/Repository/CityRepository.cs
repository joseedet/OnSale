using Microsoft.EntityFrameworkCore;

using OnSales.Common.Data;
using OnSales.Common.Entities;

using System;
using System.Threading.Tasks;

namespace OnSales.Web.Repository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DataContext _context;
        public  CityRepository(DataContext dataContext):base(dataContext)
        {
            _context = dataContext;
        }
        public Task<City> GetCitiy(int id)
        {
            throw new NotImplementedException();
        }

        public Task<City> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Department> GetDeparmentByCityAsync(City city)
        {

            //throw new NotImplementedException();
            return await _context.Departments
                .Include(d => d.Cities)
                .FirstOrDefaultAsync(c => c.Id == city.IdDepartment);
        }
    }
    
}
