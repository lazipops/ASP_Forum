using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Forum.Models
{
    public class Thread
    {
        [Key]
        public int ThreadId { get; set; }

        [Display(Name = "Posted By")]
        public string UserWhoUploaded { get; set; }

        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name ="Date Posted")]
        [DataType(DataType.Date)]
        public DateTime DateUploaded { get; set; }

        public List<Reply> Replies { get; set; }
    }
}
