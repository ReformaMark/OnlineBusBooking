using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusBooking.Models
{
    public class OriginDestinationViewModel
    {

        public int OriginID { get; set; }
        public int DestinationID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd=MMM-yyyy}")]
        public DateTime Date { get; set; }

        public int Passenger { get; set; }
    }
}