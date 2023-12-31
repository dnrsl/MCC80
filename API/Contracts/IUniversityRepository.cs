﻿using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IGeneralRepository<University>
{
    Guid GetLastUniversityGuid();

    University? GetByCode(string code);
}
