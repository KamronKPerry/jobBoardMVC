using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FPJobBoard.DATA//.Metadata
{
    public class LocationMetadata
    {
        [Required(ErrorMessage ="*Required")]
        [StringLength(10,ErrorMessage ="*Max 10 Digits")]
        [RegularExpression("\\d+",ErrorMessage ="Numeric Values Only")]
        [Display(Name ="Store Number")]
        public string StoreNumber { get; set; }
        [Required(ErrorMessage = "*Required")]
        [StringLength(50,ErrorMessage ="*Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "*Required")]
        [StringLength(2,ErrorMessage ="*Max 2 Characters")]
        public string State { get; set; }
        [Required(ErrorMessage = "*Required")]
        public string ManagerID { get; set; }


    }
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location {
        [Display(Name ="Location")]
        public string LocationName { get { return City + ", " + State; } }
    }
}
