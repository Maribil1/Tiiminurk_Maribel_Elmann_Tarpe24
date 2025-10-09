using Microsoft.AspNetCore.Mvc;
using Filminurk.Models.Movies;
using Filminurk.Data;


namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public MoviesController(FilminurkTARpe24Context context)
        {
            _context = context;
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
            MoviesCreateViewModel result=
        }

    }
}
