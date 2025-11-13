using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface1;
using Filminurk.Data;
using Filminurk.Models.UserComments;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly UserCommentServices _userCommentServices;
        public UserCommentsController(FilminurkTARpe24Context context, 
            IUserCommentServices userCommentServices)
        {
            _context = context;
            _userCommentServices= (UserCommentServices?)userCommentServices;
        }
        public IActionResult Index()
        {
            var result = _context.UserComments
                .Select(c => new UserCommentsIndexViewModel
                {
                   CommentID = c.CommentID,
                   CommentBody = c.CommentBody,
                   IsHarmful = c.IsHarmful,
                   CommentCreatedAt= c.CommentCreatedAt,
                }
                );
               return View(result);
        }
        [HttpGet]
        public IActionResult NewComment()
        {
            //todo: erista kas tegemist on admin või tava kasutajaga
            UserCommentsCreateViewModel newcomment = new();
            return View(newcomment);
        }
        [HttpPost, ActionName("NewComment")]
        //ei tohi panna allowanonymous
        public async Task<IActionResult> NewCommentPost(UserCommentsCreateViewModel newcommentVM)
        {
            //todo: newcommenti manuaalne seadmine, asenda pärast kasutaja id-ga
            //newcommentVM.CommentUserID = "00000000-0000-0000-000000000001";
            Console.WriteLine(newcommentVM.CommentUserID);
            if (ModelState.IsValid)
            {
                var dto = new UserCommentsDTO()
                { };
                dto.CommentID = newcommentVM.CommentID;
                dto.CommentBody = newcommentVM.CommentBody;
                dto.CommentUserID = newcommentVM.CommentUserID;
                dto.CommentScore = (int)newcommentVM.CommentScore;
                dto.CommentCreatedAt = newcommentVM.CommentCreatedAt;
                dto.CommentModifiedAt = newcommentVM.CommentModifiedAt;
                dto.CommentDeletedAt = newcommentVM.CommentDeletedAt;
                dto.IsHarmful = newcommentVM.IsHarmful;
                dto.IsHelpful = newcommentVM.IsHelpful;
                
                var result = await _userCommentServices.NewComment(dto);
                if (result == null)
                {
                    return NotFound();
                }
                //todo : erista ära kas tegu on admini või kasutajaga, admin
                //tagastub admin-comment-index, kasutaja aga vastava filmi juurde
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
            
        }

    }
}
