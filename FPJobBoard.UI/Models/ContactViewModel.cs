using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FPJobBoard.UI.Models
{
    public class ContactViewModel
    {
        //name, email, subject, message
        //validation, display, formatting, etc.
        //All items are found in system.ComponentModel.DataAnnotations
        [Required(ErrorMessage = "* Required")]
        [StringLength(40, ErrorMessage = "*Name cannot exceed 60 characters")]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^((([!#$%&'*+\-/=?^_`{|}~\w])|([!#$%&'*+\-/=?^_`{|}~\w][!#$%&'*+\-/=?^_`{|}~\.\w]{0,}[!#$%&'*+\-/=?^_`{|}~\w]))[@]\w+([-.]\w+)*\.\w+([-.]\w+)*)$",
            ErrorMessage = "* Sorry this email format is not accepted")]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [StringLength(50, ErrorMessage = "* Subject cannot exceed 50 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Your Message")]
        [UIHint("MultilineText")]
        public string Message { get; set; }

    }

}