using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTask.Models
{
    [Table("MsgDetailsTable")]
    public class MessageDetails
    {
        [Key]
        public int MsgID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Max 30 characters allowed")]
        [MinLength(3, ErrorMessage = "Min 3 characters allowed")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string File { get; set; }

        [StringLength(160, ErrorMessage = "Max 120 characters allowed")]
        [MinLength(20, ErrorMessage = "Min 20 characters allowed")]
        [Display(Name = "Message")]
        [RegularExpression(@"([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_])*$", ErrorMessage = "special characters are not allowed.")]
        public string Message { get; set; }

        [DataType(DataType.Time)]
        public string Time { get; set; }

        [DataType(DataType.Date)]
        public string Date { get; set; }
        
    }
}