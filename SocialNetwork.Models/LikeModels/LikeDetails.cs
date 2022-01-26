using SocialNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models.LikeModels
{
    public class LikeDetails
    {
        public int LikeId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
