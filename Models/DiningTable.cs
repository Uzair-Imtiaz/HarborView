using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborView_Inn.Models
{
    public class DiningTable
    {
        [Key]
        public int TableId { get; set; }

        public DateTime Date { get; set; }  // will contain the Date and time duration to book table in hrs

        public int noOfGuest { get; set; }

        public float Bill { get; set; }

        //[ForeignKey("User")]
        public string Email { get; set; }

        public bool isActive { get; set; }

        public string category { get; set; }

        public int bookedTable { get; set; }

    }
}
