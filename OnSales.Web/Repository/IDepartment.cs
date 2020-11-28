using OnSales.Common.Entities;

using System.Threading.Tasks;

namespace OnSales.Web.Repository
{
    public interface IDepartment:IGenericRepository<Department>
    {
        public Task<Department> GetDepartmentCities(int id);
        public Task<Department> FindByIdAsync(int id);
        public Task<Department> GetDeparmentByCityAsync(City city);
        public Task Add(Department department,City city);
        public Task <Department> GetDepartmentNotNull(int id, City city);
       


    }
}
