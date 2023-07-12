using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarborView_Inn.Models
{
    public class DiningTable
    {
        [Key]
        public int TableId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }  
        public TimeSpan time { get; set; }
        public float Bill { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public string category { get; set; }
        public string status { get; set; }
    }
}
