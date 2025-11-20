using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;

namespace Filminurk.Core.ServiceInterface1
{
    public interface IFavouriteListsServices
    {
        Task<FavouriteList> DetailsAsync(Guid id);
        Task<FavouriteList> Create(FavouriteList dto, List<Movie> selectedMovies);
    }
}
