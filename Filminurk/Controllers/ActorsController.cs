using Filminurk.Core.ServiceInterface1;
using Filminurk.Data;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        private readonly IFilesServices _filesServices;
        public ActorsController(FilminurkTARpe24Context context, IMovieServices movieServices, IFilesServices filesServices)
        {
            _context = context;
            _movieServices = movieServices;
            _filesServices = filesServices;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
