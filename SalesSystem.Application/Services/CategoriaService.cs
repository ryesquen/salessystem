using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Interfaces;

namespace SalesSystem.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _repository;
        private readonly IMapper _mapper;
        public CategoriaService(IGenericRepository<Categoria> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CategoriaDto>> GetAll()
        {
            var result = new List<CategoriaDto>();
            try
            {
                var categorias = await _repository.Query().ToListAsync();
                result = _mapper.Map<List<CategoriaDto>>(categorias);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}