﻿using SalesSystem.Application.DTOs;

namespace SalesSystem.Application.Interfaces
{
    public interface IRolService
    {
        Task<List<RolDto>> GetRoles();
    }
}