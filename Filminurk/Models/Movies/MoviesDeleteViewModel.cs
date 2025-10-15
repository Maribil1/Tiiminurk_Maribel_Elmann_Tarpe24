namespace Filminurk.Models.Movies
{
    public class MoviesDeleteViewModel
    {
        public Guid? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public string? Director { get; set; }
        public List<string>? Actors { get; set; }
        public decimal? CurrentRating { get; set; }


        //enda valitud andmed

        public int? Profit { get; set; }
        public string? Awards { get; set; }
        public string? AwardsDescription { get; set; }

        //andmebaasi jaoks vajalikud
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
