using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborView_Inn.Models
{
    public class Reservation
    {
        [Key]
        public int ResId { get; set; }
        public int noOfRooms { get; set; }

        public string ReservationName { get; set; }

        public string Category { get; set; }

        public DateTime CheckIn { get; set; }   // we will cancatenate the time by ourselves according to hotel policy

        public DateTime CheckOut { get; set; }

        public float Bill { get; set; }

        //[ForeignKey("User")]
        public string Email { get; set; }
        public bool isActive { get; set; }

        public string status { get; set; }
    }
}
