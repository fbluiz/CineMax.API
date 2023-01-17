﻿using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMovies();
    }
}
