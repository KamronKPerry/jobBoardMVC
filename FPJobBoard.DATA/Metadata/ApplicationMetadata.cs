using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FPJobBoard.DATA//.Metadata
{
    public class ApplicationMetadata
    {
        private DateTime _applicationDate = DateTime.Now;
        [Display(Name ="Application Date")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:MM/dd/yyy}")]        
        public DateTime ApplicationDate
        {
            get { return _applicationDate; }// == DateTime.Now) ? DateTime.Now : _applicationDate; }
            set { _applicationDate = value; }
        }


        [Display(Name ="Position")]
        public int OpenPositionID { get; set; }
        [Display(Name ="Applicant")]
        public string UserID { get; set; }

        [Display(Name ="Comments")]
        public string ManagerNotes { get; set; }
        [Display(Name ="Status")]
        public bool IsDeclined { get; set; }
        [Required]
        [Display(Name ="Resume")]
        public string ResumeFilename { get; set; }
    }
    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application { }
}
