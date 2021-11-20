using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Forum.Models
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }
        public int OriginPost { get; set; }

        [Required]
        [Display(Name ="Content")]
        public string Content { get; set; }

        [Display(Name = "Replied")]
        public string UserWhoReplied { get; set; }

        [Display(Name = "Date Replied")]
        [DataType(DataType.Date)]
        public DateTime DateReplied { get; set; }

        public Thread Thread { get; set; }
    }
}
