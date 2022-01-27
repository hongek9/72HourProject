using Microsoft.AspNet.Identity;
using SocialNetwork.Models.CommentModels;
using SocialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialNetwork.WebAPI.Controllers
{
    [Authorize]
    public class CommentController : ApiController
    {
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var commentService = new CommentService(userId);
            return commentService;
        }

        [HttpGet]
        public IHttpActionResult GetCommentsByPost([FromUri] int postId)
        {
            CommentService commentService = CreateCommentService();
            var comments = commentService.GetCommentsByPostId(postId);
            return Ok(comments);
        }

        [HttpGet]
        public IHttpActionResult GetCommentsByAuthor()
        {
            var service = CreateCommentService();
            var comments = service.GetCommentsByAuthor();
            return Ok(comments);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] CommentCreate comment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCommentService();
            if(!service.CreateComment(comment)) return InternalServerError();
            return Ok("Comment successful.");
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] CommentEdit comment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCommentService();
            if(!service.UpdateComment(comment)) return InternalServerError();
            return Ok("Comment updated successfully.");
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateCommentService();
            if (!service.DeleteComment(id)) return InternalServerError();
            return Ok("Comment deleted successfully.");
        }

    }
}
