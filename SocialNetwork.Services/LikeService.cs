using SocialNetwork.Data;
using SocialNetwork.Models.LikeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class LikeService
    {
        private readonly Guid _userId;

        public LikeService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateLike(LikeCreate model)
        {
            var entity = new Like()
            {
                OwnerId = _userId,
                PostId = model.PostId,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Likes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LikeDetailsInPost> GetLikesByPostId(int postId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query = ctx.Likes.Where(e => e.PostId == postId).Select(e => new LikeDetailsInPost
                {
                    LikeId = e.LikeId,
                });

                return query.ToArray();
            }
        }

        public IEnumerable<LikeDetails> GetLikesByOwnerId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Likes.Where(e => e.OwnerId == _userId).Select(e => new LikeDetails
                {
                    LikeId = e.LikeId,
                    PostId = e.PostId,
                    Post = e.Post
                });

                return query.ToArray();
            }
        }

        public bool DeleteLike(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Likes.Single(e => e.LikeId == id && e.OwnerId == _userId);

                ctx.Likes.Remove(entity);

                return ctx.SaveChanges() == 1;             
            }
        }
    }
}
