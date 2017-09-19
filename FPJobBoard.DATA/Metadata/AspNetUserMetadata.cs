using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FPJobBoard.DATA//.Metadata
{
    class AspNetUserMetadata
    {
    }
    [MetadataType(typeof(AspNetUserMetadata))]
    public partial class AspNetUser {
        [Display(Name = "Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

}
