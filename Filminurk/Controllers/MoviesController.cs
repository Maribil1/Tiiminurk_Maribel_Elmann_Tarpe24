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
        public MoviesController(FilminurkTARpe24Context context, IMovieServices movieServices)
        {
            _context = context;
            _movieServices = movieServices;
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
            return View();
        }
        [HttpGet]
        public IActionResult Create() 
        {
            MoviesCreateUpdateViewModel result = new();
            return View("Create", result);
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
                    EntryModifiedAt = viewModel.EntryModifiedAt

                };
                var result = await _movieServices.Create(dto);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return RedirectToAction(nameof(Index));


        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie=await _movieServices.DetailsAsync(id);
            if (movie==null)
            {
                return NotFound();
            }
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

            return View("CreateUpdate", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie=await _movieServices.DetailsAsync(id);
            if (movie != null)
            {
                return NotFound();
            }
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

    }
}
