using TaskCircle.UserManagerApi.DTOs;

namespace TaskCircle.UserManagerApi.Infrastructure.Services.Interfaces;

public interface IGenderService
{
    Task<GenderDTO> GetGenderById(int id);
}
