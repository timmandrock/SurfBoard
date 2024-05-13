using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SurfAndTurf.Models

{
    public class Bookings
    {
        public int ID { get; set; }
        public string IdentityUserID { get; set; }

        [Display(Name = "Board")]
        public int SurfBoardID { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public SurfBoard SurfBoard { get; set; }
        public IdentityUser IdentityUser { get; set; }


    }
}
