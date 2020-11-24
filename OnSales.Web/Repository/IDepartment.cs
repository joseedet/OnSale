using OnSales.Common.Entities;

using System.Threading.Tasks;

namespace OnSales.Web.Repository
{
    public interface IDepartment:IGenericRepository<Department>
    {
       public Task<Country> GetDepartmentCities(Department department);
    }
}
