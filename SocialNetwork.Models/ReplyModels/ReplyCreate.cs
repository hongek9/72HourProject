using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models.ReplyModels
{
    public class ReplyCreate
    {
        [Required]
        [MaxLength(1000, ErrorMessage = "This field has too many characters.")]
        public string Text { get; set; }

        [Required]
        public int CommentId { get; set; }
    }
}
