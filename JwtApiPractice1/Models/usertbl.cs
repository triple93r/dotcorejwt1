using System.ComponentModel.DataAnnotations;

namespace JwtApiPractice1.Models
{
    public class usertbl
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string passwd { get; set; }
        public string? email { get; set; }
        public string? firstname { get; set; }
    }
}
