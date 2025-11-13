using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Domain
{
    public class UserComment
    {
        [Key]
        public Guid CommentID { get; set; }
        public string? CommentUserID { get; set; }
        public string? CommentBody { get; set; }
        public int CommentScore { get; set; }
        public int? IsHelpful { get; set; }
        public int? IsHarmful { get; set; }
        public DateTime CommentCreatedAt { get; set; }
        public DateTime? CommentModifiedAt { get; set; }
        public DateTime? CommentDeletedAt { get; set; }
    } 
}
