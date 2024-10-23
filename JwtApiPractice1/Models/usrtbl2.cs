using System.ComponentModel.DataAnnotations;

namespace JwtApiPractice1.Models
{
    public class usrtbl2
    {
        [Key]
        public int id {  get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
    }
}
