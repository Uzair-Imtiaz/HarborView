using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HarborView_Inn.Models
{
    public class FoodItems
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public float Rating { get; set; }
    }
}
