using Microsoft.AspNetCore.Mvc;
using Filminurk.Models.Movies;
using Filminurk.Data;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface1;
using Microsoft.EntityFrameworkCore;


namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        private readonly IFilesServices _filesServices;
        public MoviesController(FilminurkTARpe24Context context, IMovieServices movieServices, IFilesServices filesServices)
        {
            _context = context;
            _movieServices = movieServices;
            _filesServices = filesServices;
        }

        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID=x.ID,
                Title=x.Title,
                FirstPublished=x.FirstPublished,
                CurrentRating=x.CurrentRating,
                Profit=x.Profit,
                Awards=x.Awards,
                AwardsDescription=x.AwardsDescription,
            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            MoviesCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = new MoviesDTO()
                {
                    ID = viewModel.ID,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    FirstPublished = viewModel.FirstPublished,
                    Director = viewModel.Director,
                    Actors = viewModel.Actors,
                    CurrentRating = viewModel.CurrentRating,
                    Profit = viewModel.Profit,
                    Awards = viewModel.Awards,
                    AwardsDescription = viewModel.AwardsDescription,
                    EntryCreatedAt = viewModel.EntryCreatedAt,
                    EntryModifiedAt = viewModel.EntryModifiedAt,
                    Files = viewModel.Files,
                    FilesToApiDTOs=viewModel.Images
                      .Select(x=> new FileToApiDTO
                      {
                          ImageID = x.ImageID,
                          FilePath = x.FilePath,
                          MovieID = x.MovieID,
                          IsPoster = x.IsPoster,
                      }).ToArray()

                };
                var result = await _movieServices.Create(dto);
                /*if (result != null)
                {
                   
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }*/
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var movie=await _movieServices.DetailsAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ImageViewModel[] images= await FilesFromDatabase(id);
            var vm = new MoviesDetailsViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.CurrentRating = movie.CurrentRating;
            vm.Profit = movie.Profit;
            vm.Awards = movie.Awards;
            vm.AwardsDescription = movie.AwardsDescription;
            vm.Actors = movie.Actors;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Images.AddRange(images);
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie=await _movieServices.DetailsAsync(id);
            if (movie==null)
            {
                return NotFound();
            }

            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageID = id,
                }).ToArrayAsync();        
            
            var vm = new MoviesCreateUpdateViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.CurrentRating = movie.CurrentRating;
            vm.Profit = movie.Profit;
            vm.Awards = movie.Awards;
            vm.AwardsDescription = movie.AwardsDescription;
            vm.Actors = movie.Actors;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Images.AddRange(images);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(MoviesCreateUpdateViewModel vm)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                Director = vm.Director,
                Actors = vm.Actors,
                CurrentRating = vm.CurrentRating,
                Profit = vm.Profit,
                Awards = vm.Awards,
                AwardsDescription = vm.AwardsDescription,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt,
                Files = vm.Files,
                FilesToApiDTOs= vm.Images
                .Select(x=> new FileToApiDTO()
                 {
                     ImageID = x.ImageID,
                     MovieID = x.ImageID,
                     FilePath = x.FilePath,
                 }).ToArray()
            };
            var result=await _movieServices.Update(dto);
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie=await _movieServices.DetailsAsync(id);
            if (movie != null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel()
                {
                    FilePath = y.ExistingFilePath,
                    ImageID = y.ImageID,
                }).ToArrayAsync();

            var vm = new MoviesDeleteViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.CurrentRating = movie.CurrentRating;
            vm.Profit = movie.Profit;
            vm.Awards = movie.Awards;
            vm.AwardsDescription = movie.AwardsDescription;
            vm.Actors= movie.Actors;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Images.AddRange(images);
              
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var movie = await _movieServices.Delete(id);
            if (movie != null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task<ImageViewModel[]> FilesFromDatabase(Guid id)
        {
            return await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                    ImageID = y.ImageID,
                    MovieID = y.MovieID,
                    IsPoster = y.IsPoster,
                    FilePath = y.ExistingFilePath
                }).ToArrayAsync();
        }

    }
}
