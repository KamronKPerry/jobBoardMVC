using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FPJobBoard.DATA.Metadata
{
    public class LocationMetadata
    {
        public string StoreNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ManagerID { get; set; }


    }
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }
}
