using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FPJobBoard.DATA.Metadata
{
    public class PositionMetadata
    {
        [Display(Name ="Job Title")]
        [Required(ErrorMessage ="*Required")]
        public string Title { get; set; }
        [Display(Name ="Job Description")]
        public string JobDescription { get; set; }
    }
    [MetadataType(typeof(PositionMetadata))]
    public partial class Position { }

}
