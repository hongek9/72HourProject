using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Models.CommentModels;
using SocialNetwork.Models.LikeModels;
using SocialNetwork.Models.PostModels;
using SocialNetwork.Models.ReplyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity = new Post()
            {
                AuthorId = _userId,
                Title = model.Title,
                Text = model.Text,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostDetails> GetPosts()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query = ctx.Posts.Select(e => new PostDetails
                {
                    PostId = e.PostId,
                    Title = e.Title,
                    Text = e.Text,
                    Comments = e.Comments.Select(comment => new CommentInPostDetails()
                    {
                        CommentId = comment.CommentId,
                        Text = comment.Text,
                   
                        Replies = comment.Replies.Select(reply => new ReplyInCommentDetails()
                        {
                            ReplyId = reply.ReplyId,
                            Text = reply.Text
                        }).ToList()
                    }).ToList(),
                    Likes = e.Likes.Select(like => new LikeDetailsInPost()
                    {
                        LikeId = like.LikeId
                    }).ToList()
                }); 

                return query.ToArray();
            }
        }

        public IEnumerable<PostDetails> GetPostByAuthorId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Posts.Where(e => e.AuthorId == _userId).Select(e => new PostDetails
                {
                    PostId = e.PostId,
                    Title = e.Title,
                    Text = e.Text,
                    Comments = e.Comments.Select(comment => new CommentInPostDetails()
                    {
                        CommentId = comment.CommentId,
                        Text = comment.Text,

                        Replies = comment.Replies.Select(reply => new ReplyInCommentDetails()
                        {
                            ReplyId = reply.ReplyId,
                            Text = reply.Text
                        }).ToList()
                    }).ToList(),
                    Likes = e.Likes.Select(like => new LikeDetailsInPost() { 
                        LikeId = like.LikeId
                    }).ToList()
                });

                return query.ToArray();
            }
        }

        public bool UpdatePost(PostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts.Single(e => e.PostId == model.PostId && e.AuthorId == _userId);

                entity.PostId = model.PostId;
                entity.Title = model.Title;
                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts.Single(e => e.PostId == postId && e.AuthorId == _userId);

                ctx.Posts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
