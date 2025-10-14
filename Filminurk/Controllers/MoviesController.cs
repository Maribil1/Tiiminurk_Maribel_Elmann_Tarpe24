using Microsoft.AspNetCore.Mvc;
using Filminurk.Models.Movies;
using Filminurk.Data;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface1;


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
            MoviesCreateViewModel result = new();
            return View("Create", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateViewModel viewModel)
        {
            var dto = new MoviesDTO()
            {
                ID=viewModel.ID,
                Title=viewModel.Title,
                Description=viewModel.Description,
                FirstPublished=viewModel.FirstPublished,
                Director=viewModel.Director,
                Actors=viewModel.Actors,
                CurrentRating=viewModel.CurrentRating,
                Profit=viewModel.Profit,
                Awards=viewModel.Awards,
                AwardsDescription=viewModel.AwardsDescription,
                EntryCreatedAt=viewModel.EntryCreatedAt,
                EntryModifiedAt=viewModel.EntryModifiedAt

            };
            var result=await _movieServices.Create(dto);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
