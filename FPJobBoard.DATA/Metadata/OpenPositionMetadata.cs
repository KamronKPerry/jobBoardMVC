using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FPJobBoard.DATA//.Metadata
{
    public class OpenPositionMetadata
    {
        
        public int PositionID { get; set; }
        public int LocationID { get; set; }
        [Display(Name = "Is Hiring?")]
        public bool IsOpen { get; set; }
    }
    [MetadataType(typeof(OpenPositionMetadata))]
    public partial class OpenPosition {
        [Display(Name ="Position")]
    public string PositionName { get { return Position.Title + " - " + Location.LocationName; } }
            
            }
}
