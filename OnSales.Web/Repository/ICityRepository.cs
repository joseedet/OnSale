using OnSales.Common.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSales.Web.Repository
{
    public interface ICityRepository:IGenericRepository<City>
    {
        public Task<City> GetCitiy(int id);
        public Task<City> FindByIdAsync(int id);
        public Task<Department> GetDeparmentByCityAsync(City city);
    }
}
