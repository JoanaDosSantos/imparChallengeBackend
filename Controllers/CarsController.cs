using Microsoft.AspNetCore.Mvc;
using imparChallenge.Services.Cars;
using imparChallenge.Models;
using imparChallenge.Models.DTOs;

namespace imparChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carsService;

        public CarsController(ICarService carsService) { _carsService = carsService; }


        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _carsService.GetCarAsync(id);
            if (car == null) return NotFound();

            return Ok(car);
        }

        [HttpGet("search")]
        public async Task<CarsDataResponse> SearchCar(string? term, int limit = 12, int offset = 0)
        {
            return new CarsDataResponse
            {
                Cars = await _carsService.GetCarsAsync(limit, offset, term),
                TotalCars = await _carsService.GetTotalCarsAsync(term)
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            try
            {
                await _carsService.UpdateCar(id, car);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            try
            {
                await _carsService.AddCar(car);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carsService.DeleteCar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }
    }
}
