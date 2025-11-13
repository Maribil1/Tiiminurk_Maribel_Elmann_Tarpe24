using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface1;
using Filminurk.Data;

namespace Filminurk.ApplicationServices.Services
{
    public class UserCommentServices: IUserCommentServices
    {
        private readonly FilminurkTARpe24Context _context;
        public UserCommentServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public async Task<UserComment> NewComment(UserCommentsDTO newcommentDTO)
        {
            UserComment domain = new UserComment();

            domain.CommentID=Guid.NewGuid();
            domain.CommentBody=newcommentDTO.CommentBody;
            domain.CommentUserID= newcommentDTO.CommentUserID;
            domain.CommentCreatedAt= newcommentDTO.CommentCreatedAt;
            domain.CommentModifiedAt= newcommentDTO.CommentModifiedAt;
            domain.IsHelpful= 0;
            domain.IsHarmful=0;
            
            await _context.UserComments.AddAsync(domain);
            await _context.SaveChangesAsync();
            return domain;
        }
    }
}
