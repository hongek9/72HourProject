using Microsoft.AspNet.Identity;
using SocialNetwork.Models;
using SocialNetwork.Models.PostModels;
using SocialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialNetwork.WebAPI.Controllers
{
    public class PostController : ApiController
    {
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postService = new PostService(userId);
            return postService;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            PostService postService = CreatePostService();
            var posts = postService.GetPosts();
            return Ok(posts);
        }

        [HttpGet]
        public IHttpActionResult GetByAuthorId()
        {
            PostService postService = CreatePostService();
            var posts = postService.GetPostByAuthorId();
            return Ok(posts);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]PostCreate post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreatePostService();

            if (!service.CreatePost(post)) return InternalServerError();

            return Ok("Post successfully created.");
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] PostEdit post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreatePostService();

            if (!service.UpdatePost(post)) return InternalServerError();

            return Ok("Post successfully updated.");
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreatePostService();

            if (!service.DeletePost(id)) return InternalServerError();

            return Ok("Post successfully deleted.");
        }
    }
}
