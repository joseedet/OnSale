using OnSales.Common.Entities;

using System.Linq;
using System.Threading.Tasks;

namespace OnSales.Web.Repository
{
    public interface ICountriesRepository:IGenericRepository<Country>
    {
        IQueryable<Country> ListCountries();
        Task<Country> GetAllDepartmentAndCities(int id);
        Task<Country> GetCountryDepartment(Department department);
        Task<Country> GetCountryDepartmentNotNull(Department department);
        Task<Country> FindByIdAsync(int id);


    }
}
