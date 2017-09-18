using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FPJobBoard.DATA.Metadata
{
    public class ApplicationMetadata
    {


        public int OpenPositionID { get; set; }
        public string UserID { get; set; }
        public System.DateTime ApplicationDate { get; set; }
        [Display(Name ="Comments")]
        public string ManagerNotes { get; set; }
        [Display(Name ="Status")]
        public bool IsDeclined { get; set; }
        public string ResumeFilename { get; set; }
    }
    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application { }
}
