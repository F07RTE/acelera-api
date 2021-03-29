using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DatabaseContext _context;

        public CountryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> GetAll()
        {
            return _context.Country.ToList();
        }

        public Country Get(int id)
        {
            return _context.Country.FirstOrDefault(p => p.id == id);
        }

        public void Create(Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            _context.Country.Add(country);
        }

        public void Update(Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            _context.Country.Update(country);
        }

        public void Delete(Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            _context.Remove(country);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}