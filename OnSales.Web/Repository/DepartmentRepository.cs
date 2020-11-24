using OnSales.Common.Data;
using OnSales.Common.Entities;

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

      

      

        Task<Country> IDepartment.GetDepartmentCities(Department department)
        {
            throw new System.NotImplementedException();
        }
    }
}
