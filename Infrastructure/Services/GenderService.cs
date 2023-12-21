using AutoMapper;
using TaskCircle.UserManagerApi.DTOs;
using TaskCircle.UserManagerApi.Infrastructure.Repositories.Interfaces;
using TaskCircle.UserManagerApi.Infrastructure.Services.Interfaces;

namespace TaskCircle.UserManagerApi.Infrastructure.Services
{
    public class GenderService : IGenderService
    {

        private readonly IGenderRepository? _genderRepository;
        private readonly IMapper? _mapper;

        public async Task<GenderDTO?> GetGenderById(int id)
        {
            var genderEntity = await _genderRepository.getById(id);
            return _mapper?.Map<GenderDTO>(genderEntity);
        }
    }
}
