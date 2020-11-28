using Microsoft.EntityFrameworkCore;

using OnSales.Common.Data;
using OnSales.Common.Entities;

using System.Linq;
using System.Threading.Tasks;

namespace OnSales.Web.Repository
{
    public class DepartmentRepository:GenericRepository<Department>,IDepartment
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext dataContext):base(dataContext)
        {
            _context = dataContext;
        }

        public async Task Add(Department department, City city)
        {
            department.Cities.Add(city);
            _context.Update(department);
           await _context.SaveChangesAsync();
            
        }

        public async Task<Department> FindByIdAsync(int id)
        {
             return await  _context.Departments.FindAsync(id);           
        }

        public Task<Country> GetDeparmentByCityAsync(Department department)
        {
           
            throw new System.NotImplementedException();
        }

        public async Task<Department> GetDeparmentByCityAsync(City city)
        {
           return await _context.Departments
               .Include(d => d.Cities)
               .FirstOrDefaultAsync(c => c.Id == city.IdDepartment);
        }

        public async Task<Department> GetDepartmentCities(int id)
        {
             return await  _context.Departments
            .Include(d => d.Cities)
            .FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<Department> GetDepartmentNotNull(int id, City city)
        {
            //return await _context.Departments.FirstOrDefaultAsync(d=> d.Citi) != null);
            return await _context.Departments.Include(d => d.Cities)
                         .FirstOrDefaultAsync(d => d.Cities.FirstOrDefault(c => c.Id == city.Id) != null);
        }

       
    }
}
