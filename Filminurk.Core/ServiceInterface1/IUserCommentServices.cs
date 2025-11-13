using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface1
{
    public interface IUserCommentServices
    {
        Task<UserComment> NewComment(UserCommentsDTO newcommentDTO);
        Task<UserComment> DetailAsync(Guid id);
        Task<UserComment> Delete(Guid id);
    }
}
