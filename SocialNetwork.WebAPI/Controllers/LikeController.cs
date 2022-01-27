using Microsoft.AspNet.Identity;
using SocialNetwork.Models.LikeModels;
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
    public class LikeController : ApiController
    {
        private LikeService CreateLikeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var likeService = new LikeService(userId);
            return likeService;
        }

        [HttpGet]
        public IHttpActionResult GetByPostId([FromUri]int postId)
        {
            var service = CreateLikeService();
            var likes = service.GetLikesByPostId(postId);
            return Ok(likes);
        }

        [HttpGet]
        public IHttpActionResult GetPostByOwner()
        {
            var service = CreateLikeService();
            var likes = service.GetLikesByOwnerId();
            return Ok(likes);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] LikeCreate model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateLikeService();

            if (!service.CreateLike(model)) return InternalServerError();

            return Ok("Like successful.");
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateLikeService();
            if (!service.DeleteLike(id)) return InternalServerError();
            return Ok("Like Delete Successful.");
        }
    }
}
