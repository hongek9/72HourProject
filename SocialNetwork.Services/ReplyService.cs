using SocialNetwork.Data;
using SocialNetwork.Models.ReplyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class ReplyService
    {
        private readonly Guid _userId;

        public ReplyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateReply(ReplyCreate model)
        {
            var entity = new Reply()
            {
                AuthorId = _userId,
                Text = model.Text,
                CommentId = model.CommentId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Replies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReplyDetails> GetRepliesByCommentId(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Replies.Where(e => e.CommentId == commentId).Select(e => new ReplyDetails
                {
                    ReplyId = e.ReplyId,
                    Text = e.Text,
                });

                return query.ToArray();
            }
        }

        public IEnumerable<ReplyDetails> GetRepliesByAuthorId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Replies.Where(e => e.AuthorId == _userId).Select(e => new ReplyDetails
                {
                    ReplyId = e.ReplyId,
                    Text = e.Text,
                    CommentId = e.CommentId,
                    Comment = e.Comment
                });

                return query.ToArray();
            }
        }

        public bool UpdateReply(ReplyEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Replies.Single(e => e.ReplyId == model.ReplyId && e.AuthorId == _userId);

                entity.ReplyId = model.ReplyId;
                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteReply(int replyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Replies.Single(e => e.ReplyId == replyId && e.AuthorId == _userId);

                ctx.Replies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
