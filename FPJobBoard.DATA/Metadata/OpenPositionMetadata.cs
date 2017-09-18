using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FPJobBoard.DATA.Metadata
{
    public class OpenPositionMetadata
    {
        public int PositionID { get; set; }
        public int LocationID { get; set; }
        [Display(Name = "Is Hiring?")]
        [UIHint("CheckBox")]
        public bool IsOpen { get; set; }
    }
    [MetadataType(typeof(OpenPositionMetadata))]
    public partial class OpenPosition { }
}
