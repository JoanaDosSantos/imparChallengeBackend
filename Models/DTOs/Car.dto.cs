using imparChallenge.Models;

namespace imparChallenge.Models.DTOs
{
    public class CarsDataResponse
    {
        public List<Car> Cars { get; set; } = new List<Car>();
        public int TotalCars { get; set; }
    }
}
