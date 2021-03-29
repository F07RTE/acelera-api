using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class MockCountryRepository : ICountryRepository
    {
        public void Create(Country country)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Country> GetAll()
        {
            var countries = new List<Country>
            {
                new Country { id = 1, name = "Brazil", capital = "Pelé", isAvalible = true },
                new Country { id = 2, name = "United States", capital = "American soccer", isAvalible = false },
                new Country { id = 3, name = "Japan", capital = "Sushi", isAvalible = false }
            };

            return countries;
        }

        public Country Get(int id)
        {
            return new Country() { id = 1, name = "Brazil", capital = "Pelé", isAvalible = true };
        }

        public void Update(Country country)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Country country)
        {
            throw new System.NotImplementedException();
        }
    }
}