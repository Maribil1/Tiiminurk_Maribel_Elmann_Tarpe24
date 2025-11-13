using Filminurk.Core.Domain;

namespace Filminurk.Models.Movies
{
    public class MoviesCreateUpdateViewModel
    {
        public Guid? ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }
        public string Director { get; set; }
        public List<Actor>? Actors{ get; set; }
        public decimal? CurrentRating { get; set; }
        /* kaasasolevate piltide andmeomadused */
        public List<IFormFile>? Files{ get; set; }
        public List<ImageViewModel>? Images{ get; set; } = new List<ImageViewModel>();


        //enda valitud andmed

        public int? Profit { get; set; }
        public string? Awards { get; set; }
        public string? AwardsDescription { get; set; }

        //andmebaasi jaoks vajalikud
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
