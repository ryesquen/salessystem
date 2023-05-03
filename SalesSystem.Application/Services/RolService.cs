using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Interfaces;

namespace SalesSystem.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _repository;
        private readonly IMapper _mapper;

        public RolService(IMapper mapper, IGenericRepository<Rol> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<RolDto>> GetRoles()
        {
            var result = new List<RolDto>();
            try
            {
                var roles = await _repository.Query().ToListAsync();
                result = _mapper.Map<List<RolDto>>(roles);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
