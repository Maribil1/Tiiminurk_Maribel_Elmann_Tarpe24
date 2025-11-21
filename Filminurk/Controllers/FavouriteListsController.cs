using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface1;
using Filminurk.Data;
using Filminurk.Models.FavouriteLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavouriteListsServices _favouriteListsServices;
        //fileservice add later
        public FavouriteListsController(FilminurkTARpe24Context context, IFavouriteListsServices favouriteListsServices)
        {
            _context = context;
            _favouriteListsServices = favouriteListsServices;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.FavouriteLists
                .OrderByDescending(y => y.ListCreatedAt)
                .Select(x => new FavouriteListsIndexViewModel
                {
                    FavouriteListID = x.FavouriteListID,
                    ListBelongsToUser = x.ListBelongsToUser,
                    ListCreatedAt = x.ListCreatedAt,
                    ListDescription = x.ListDescription,
                    ListName = x.ListName,
                    IsMoviesOrActor = x.IsMoviesOrActor,
                    Image = (List<FavouriteListIndexImageViewModel>)_context.FilesToDatabase
                    .Where(ml => ml.ListID == x.FavouriteListID)
                    .Select(li => new FavouriteListIndexImageViewModel()
                    {
                        ImageID = li.ImageID,
                        ListID = li.ListID,
                        ImageData = li.ImageData,
                        ImageTitle = li.ImageTitle,
                        Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(li.ImageData)),
                    })
                });
            return View(resultingLists);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //todo: identify user type. return diffrent views for admin and user
            var movies=_context.Movies
                .OrderBy(m => m.Title)
                .Select(m=> new MoviesIndexViewModel
                {
                    ID = m.ID,
                    Title = m.Title,
                    FirstPublished = m.FirstPublished,
                    Awards = m.Awards,

                })
                .ToList();
            ViewData["allmovies"] = movies;
            ViewData["userHasSelected"]=new List<string>();
            //for normal  user
            FavouriteListsUserCreateViewModel vm = new();
            return View("UserCreate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(FavouriteListsUserCreateViewModel vm, List<string> userHasSelected,
            List<MoviesIndexViewModel> movies)
        {
            List<Guid> tempParse = new();
            foreach (var stringID in userHasSelected) 
            { 
              tempParse.Add(Guid.Parse(stringID));
            }
            var newListDto = new FavouriteListDTO() { };
            newListDto.ListName=vm.ListName;
            newListDto.ListDescription=vm.ListDescription;
            newListDto.IsMoviesOrActor=vm.IsMoviesOrActor;
            newListDto.IsPrivate=vm.IsPrivate;
            newListDto.ListCreatedAt= (DateTime)vm.ListCreatedAt;
            newListDto.ListBelongsToUser = "00000000-0000-000-000-000000000001";
            newListDto.ListModifiedAt=DateTime.UtcNow;
            newListDto.ListDeletedAt= vm.ListDeletedAt;
            newListDto.ListOfMovies= vm.ListOfMovies;

            var listofmoviestoadd = new List<Movie>();
            foreach (var movieId in tempParse) 
            {
                var thismovie = _context.Movies.Where(tm => tm.ID == movieId).ToList().Take(1);
                newListDto.ListOfMovies.Add((Movie)thismovie);
            }
            newListDto.ListOfMovies = listofmoviestoadd;
            //List<Guid>convertedIDS = new List<Guid>();
            //if (newListDto.ListOfMovies !=null)
            //{
            //    convertedIDS= MovieToId(newListDto.ListOfMovies);
            //}
            var newList = await _favouriteListsServices.Create(newListDto  /*convertedIDS*/);
            if (newList != null) 
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);
        }

        private List<Guid> MovieToId(List<Movie> listOfMovies)
        {
            var results= new List<Guid>();
            foreach (var movie in listOfMovies) 
            { 
              results.Add(movie.ID);
            }
            return results;
        }
        public async Task<FavouriteList> Create(FavouriteList dto, List<Movie> selectedMovies)
        {
            FavouriteList newList = new();
            newList.FavouriteListID=Guid.NewGuid();
            newList.ListName=dto.ListName;
            newList.ListDescription=dto.ListDescription;
            newList.ListCreatedAt=dto.ListCreatedAt;
            newList.ListModifiedAt=dto.ListModifiedAt;
            newList.ListDeletedAt=dto.ListDeletedAt;
            //newList.ListOfMovies=selectedMovies;
            await _context.FavouriteLists.AddAsync(newList);
            await _context.SaveChangesAsync();

            //foreach (var movieid in selectedMovies) 
            //{
               // _context.FavouriteLists.Entry

            //}
            return newList;
        }
    }
}
