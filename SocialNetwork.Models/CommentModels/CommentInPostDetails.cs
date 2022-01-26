using SocialNetwork.Models.ReplyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models.CommentModels
{
    public class CommentInPostDetails
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public IEnumerable<ReplyInCommentDetails> Replies { get; set; }
    }
}
