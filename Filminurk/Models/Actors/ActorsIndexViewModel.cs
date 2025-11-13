using static Filminurk.Core.Domain.Actor;

namespace Filminurk.Models.Actors
{
    public class ActorsIndexViewModel
    {
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string>? NickName { get; set; }
        public Guid PortraitID { get; set; }

        public int? Age { get; set; }
        public string? Gender { get; set; }
        public Child? Children { get; set; }
    }
}
