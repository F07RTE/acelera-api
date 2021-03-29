using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi.Models;
using TodoApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNet.OData;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryRepository _repository;
        private readonly IMapper _mapper;

        public CountryController(
            ILogger<CountryController> logger,
            ICountryRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // HEAD /country
        [HttpHead("{id}")]
        public ActionResult Head(int id)
        {
            var country = _repository.Get(id);
            if (country != null)
            {
                return Ok();
            }

            return NotFound();
        }

        // GET /country
        [HttpGet]
        [EnableQuery()]
        public ActionResult<IEnumerable<CountryReadDto>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CountryReadDto>>(_repository.GetAll()));
        }

        // GET /country/{id}
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<CountryReadDto> Get(int id)
        {
            var country = _repository.Get(id);
            if (country != null)
            {
                return Ok(_mapper.Map<CountryReadDto>(country));
            }

            return NotFound();
        }

        // POST /country
        [HttpPost]
        public ActionResult<CountryReadDto> Post(CountryCreateDto country)
        {
            var countryModel = _mapper.Map<Country>(country);
            _repository.Create(countryModel);
            _repository.SaveChanges();

            var countryReadDto = _mapper.Map<CountryReadDto>(countryModel);

            return CreatedAtRoute(nameof(Get), new { Id = countryReadDto.id }, countryReadDto);
        }

        // PUT /country/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, CountryUpdateDto country)
        {
            var countryFromRepository = _repository.Get(id);
            if (countryFromRepository == null)
            {
                return NotFound();
            }

            _mapper.Map(country, countryFromRepository);

            _repository.Update(countryFromRepository);
            _repository.SaveChanges();

            return Ok(countryFromRepository);
        }

        // PATCH /country/{id}
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, JsonPatchDocument<CountryUpdateDto> patchCountry)
        {
            var countryFromRepository = _repository.Get(id);
            if (countryFromRepository == null)
            {
                return NotFound();
            }

            var countryToPatch = _mapper.Map<CountryUpdateDto>(countryFromRepository);
            patchCountry.ApplyTo(countryToPatch, ModelState);

            if (!TryValidateModel(countryToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(countryToPatch, countryFromRepository);

            _repository.Update(countryFromRepository);
            _repository.SaveChanges();

            return Ok(countryFromRepository);
        }

        // DELETE /country
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var countryFromRepository = _repository.Get(id);
            if (countryFromRepository == null)
            {
                return NotFound();
            }

            _repository.Delete(countryFromRepository);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpOptions]
        public ActionResult Options()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", new[] { "https://localhost:5001" });
            Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type: application/json; charset=utf-8" });
            Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, PATCH, DELETE, OPTIONS" });
            Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "false" });

            return Ok();
        }
    }
}