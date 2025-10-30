using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Filminurk.Core.Domain.Actor;

namespace Filminurk.Core.Dto
{
    public class ActorsDTO
    {
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        List<string>? NickName { get; set; }
        public Guid PortraitID { get; set; }

        public int? Age { get; set; }
        public string? Gender { get; set; }
        public Child? Children { get; set; }
    }
}
