namespace Filminurk.Models.UserComments
{
    public class UserCommentsCreateViewModel
    {
        public Guid CommentID { get; set; }
        public string CommentUserID { get; set; } = "00000000-0000-0000-000000000001";
        public string CommentBody { get; set; }
        public int? CommentScore { get; set; }
        public int? IsHelpful { get; set; }
        public int? IsHarmful { get; set; }
        public DateTime CommentCreatedAt { get; set; }
        public DateTime CommentModifiedAt { get; set; }
        public DateTime? CommentDeletedAt { get; set; }
    }
}
