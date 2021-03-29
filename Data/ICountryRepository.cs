using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Data
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAll();
        Country Get(int id);
        void Create(Country country);
        void Update(Country country);
        void Delete(Country country);
        bool SaveChanges();
    }
}