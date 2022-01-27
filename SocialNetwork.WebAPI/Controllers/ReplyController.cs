using Microsoft.AspNet.Identity;
using SocialNetwork.Models.ReplyModels;
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
    public class ReplyController : ApiController
    {
        private ReplyService CreateReplyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var replyService = new ReplyService(userId);
            return replyService;
        }

        [HttpGet]
        public IHttpActionResult GetByCommentId([FromUri] int commentId)
        {
            var service = CreateReplyService();
            var replies = service.GetRepliesByCommentId(commentId);
            return Ok(replies);
        }

        [HttpGet]
        public IHttpActionResult GetByAuthorId()
        {
            var service = CreateReplyService();
            var replies = service.GetRepliesByAuthorId();
            return Ok(replies);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] ReplyCreate reply)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateReplyService();

            if (!service.CreateReply(reply)) return InternalServerError();

            return Ok("Reply created successfully.");
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] ReplyEdit model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateReplyService();

            if (!service.UpdateReply(model)) return InternalServerError();

            return Ok("Reply successfully updated.");
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateReplyService();
            if (!service.DeleteReply(id)) return InternalServerError();
            return Ok("Deleted reply successfully.");
           
        }
    }
}
