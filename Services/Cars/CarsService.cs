using imparChallenge.Models;
using imparChallenge.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace imparChallenge.Services.Cars
{
    public interface ICarService
    {
        Task AddCar(Car newCar);
        Task<Car> GetCarAsync(int carId);
        Task<List<Car>> GetCarsAsync(int limit, int offset, string? term);
        Task<int> GetTotalCarsAsync(string? term);
        Task DeleteCar(int carId);
        Task UpdateCar(int carId, Car car);
    }
    public class CarsService : ICarService
    {
        private readonly ImparTestContext _context;

        public CarsService(ImparTestContext context) { _context = context; }

        public async Task AddCar(Car newCar)
        {
            await _context.Cars.AddAsync(newCar);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetCarAsync(int carId)
        {
            return await _context.Cars
                .Include(car => car.photo)
                .Where(car => car.Id == carId)
                .FirstAsync();
        }

        public async Task<List<Car>> GetCarsAsync(int limit, int offset, string? term)
        {
            return await _context.Cars
                .Include(car => car.photo)
                .Where(car => term != null ? car.Name.Contains(term) : true)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> GetTotalCarsAsync(string? term)
        {
            return await _context.Cars
                 .Where(car => term != null ? car.Name.Contains(term) : true)
                 .CountAsync();
        }

        public async Task DeleteCar(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null) throw new Exception("Erro ao encontrar carro");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCar(int carId, Car car)
        {
            var _car = await _context.Cars
                .Include(car => car.photo)
                .Where(car => car.Id == carId)
                .FirstOrDefaultAsync();
            if (_car == null) throw new Exception("Erro ao encontrar carro");

            _car.Name = car.Name;
            _car.Status = car.Status;
            _car.photo.Base64 = car.photo.Base64;

            _context.Entry(_car);
            await _context.SaveChangesAsync();
        }
    }
}
