using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models
{
    public class Value
    {
        
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }    
    }
}