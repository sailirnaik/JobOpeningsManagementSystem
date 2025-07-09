using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;

        public LocationService(ILocationRepository repository)
        {
            _repository = repository;
        }

        public void Create(LocationCreateDTO dto)
        {
            var location = new LocationModel
            {
                Title = dto.Title,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                Zip = dto.Zip
            };

            _repository.Add(location);
            _repository.Save();
        }

        public void Update(int id, LocationCreateDTO dto)
        {
            var location = _repository.GetById(id);
            if (location == null)
                throw new Exception("Location not found");

            location.Title = dto.Title;
            location.City = dto.City;
            location.State = dto.State;
            location.Country = dto.Country;
            location.Zip = dto.Zip;

            _repository.Update(location);
            _repository.Save();
        }

        public List<LocationDTO> GetAll()
        {
            var locations = _repository.GetAll();
            return locations.Select(l => new LocationDTO
            {
                Id = l.Id,
                Title = l.Title,
                City = l.City,
                State = l.State,
                Country = l.Country,
                Zip = l.Zip
            }).ToList();
        }
    }
}
