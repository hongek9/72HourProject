using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models.PostModels
{
    public class PostEdit
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
