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
    public class FavouriteListsServices : IFavouriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;

        public FavouriteListsServices(FilminurkTARpe24Context context, IFilesServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
        }

        public async Task<FavouriteList> Create(FavouriteListDTO dto /* List<Movie> selectedMovies*/)
        {
            FavouriteList newList = new();
            newList.FavouriteListID = Guid.NewGuid();
            newList.ListName = dto.ListName;
            newList.ListDescription = dto.ListDescription;
            newList.IsMoviesOrActor = dto.IsMoviesOrActor;
            newList.ListModifiedAt = dto.ListModifiedAt;
            newList.ListCreatedAt = dto.ListCreatedAt;
            newList.ListDeletedAt = dto.ListDeletedAt;
            await _context.FavouriteLists.AddAsync(newList);
            await _context.SaveChangesAsync();

            return newList;
        }

        //public Task<FavouriteList> Create(FavouriteListDTO dto)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<FavouriteList> DetailsAsync(Guid id)
        {
            var result= await _context.FavouriteLists
                .FirstOrDefaultAsync(x => x.FavouriteListID == id);
            return result;
        }
     

    }
}
