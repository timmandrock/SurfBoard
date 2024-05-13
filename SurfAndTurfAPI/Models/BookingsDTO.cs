using System.ComponentModel.DataAnnotations;

namespace SurfAndTurfAPI.Models
{
    public class BookingsDTO
    {
        public string UserName { get; set; }

        [Display(Name = "Board")]
        public int SurfBoardID { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
