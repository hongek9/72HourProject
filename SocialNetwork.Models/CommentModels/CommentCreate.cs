using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models.CommentModels
{
    public class CommentCreate
    {
        [Required]
        [MaxLength(1000, ErrorMessage = "There are too many characters in this field.")]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }

    }
}
