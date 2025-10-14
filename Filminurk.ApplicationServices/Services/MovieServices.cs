using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface1;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class MovieServices : IMovieServices
    {
        public readonly FilminurkTARpe24Context _context;

        public MovieServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public async Task<Movie> Create(MoviesDTO dto)
        {
            Movie movie= new Movie();
            movie.ID= Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.FirstPublished=(DateOnly)dto.FirstPublished;
            movie.Director = dto.Director;
            movie.CurrentRating = dto.CurrentRating;
            movie.Actors = dto.Actors;
            movie.Profit = dto.Profit;
            movie.Awards = dto.Awards;
            movie.AwardsDescription = dto.AwardsDescription;
           // movie.EntryCreatedAt=DateTime.Now;
           //movie.EntryModifiedAT=DateTime.Now;
           await _context.Movies.AddAsync(movie);
           await _context.SaveChangesAsync();
           
           return movie;
        }
        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result= await _context.Movies.FirstOrDefaultAsync(x => x.ID==id);
            return result;
        }

    }
}
