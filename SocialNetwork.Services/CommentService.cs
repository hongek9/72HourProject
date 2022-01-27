using SocialNetwork.Data;
using SocialNetwork.Models.CommentModels;
using SocialNetwork.Models.ReplyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity = new Comment()
            {
                AuthorId = _userId,
                Text = model.Text,
                PostId = model.PostId,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentDetails> GetCommentsByPostId(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Comments.Where(e => e.PostId == postId).Select(e => new CommentDetails
                {
                    CommentId = e.CommentId,
                    Text = e.Text,
                    PostId = e.PostId,
                    Post = e.Post,
                    Replies = e.Replies.Where(reply => reply.CommentId == e.CommentId).Select(reply => new ReplyInCommentDetails()
                    {
                        ReplyId = reply.ReplyId,
                        Text = reply.Text,
                    }).ToList()
                });

                return query.ToArray();
            }
        }

        public IEnumerable<CommentDetails> GetCommentsByAuthor()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Comments.Where(e => e.AuthorId == _userId).Select(e => new CommentDetails
                {
                    CommentId = e.CommentId,
                    Text = e.Text,
                    PostId = e.PostId,
                    Post = e.Post,
                    Replies = e.Replies.Where(reply => reply.CommentId == e.CommentId).Select(reply => new ReplyInCommentDetails()
                    {
                        ReplyId = reply.ReplyId,
                        Text = reply.Text,
                    }).ToList()
                });

                return query.ToArray();
            }
        }

        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Comments.Single(e => e.CommentId == model.CommentId && e.AuthorId == _userId);

                entity.CommentId = model.CommentId;
                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteComment(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Comments.Single(e => e.CommentId == id && e.AuthorId == _userId);

                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
