using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class Notification:BaseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        public string Title { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Description { get; set; }
        public DateTime? DateAndTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string[] Users { get; set; }
        public string SenderUser { get; set; } = "";

    }
}
