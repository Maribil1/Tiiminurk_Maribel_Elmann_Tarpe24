namespace Filminurk.Views.Movies
{
    public class MoviesCreateViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateOnly FirstPublished { get; set; }
        public decimal? CurrentRating { get; set; }


        //enda valitud andmed

        public int? Profit { get; set; }
        public string? Awards { get; set; }
        public string? AwardsDescription { get; set; }

        //andmebaasi jaoks vajalikud
        public DateTime? Entry { get; set; }
    }
}
