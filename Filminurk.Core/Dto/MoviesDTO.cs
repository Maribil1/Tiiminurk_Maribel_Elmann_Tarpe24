using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Microsoft.AspNetCore.Http;

namespace Filminurk.Core.Dto
{
    public class MoviesDTO
    {
        public Guid? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public string? Director { get; set; }
        public List<Actor>? Actors { get; set; }
        public decimal? CurrentRating { get; set; }

        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToApiDTO> FilesToApiDTOs { get; set; } = new List<FileToApiDTO>();

        //enda valitud andmed

        public int? Profit { get; set; }
        public string? Awards { get; set; }
        public string? AwardsDescription { get; set; }

        //andmebaasi jaoks vajalikud
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}
