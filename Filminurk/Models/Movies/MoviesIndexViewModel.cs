namespace Filminurk.Models.Movies
{
    public class MoviesIndexViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateOnly FirstPublished { get; set; }
        public double? CurrentRating { get; set; }


        //enda valitud andmed

        public int? Profit { get; set; }
        public string? Awards { get; set; }
        public string? AwardsDescription { get; set; }
    }

}

