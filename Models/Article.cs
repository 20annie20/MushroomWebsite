using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MushroomWebsite.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifiedAt { get; set; }
        [StringLength(1024)]
        public string Text { get; set; }
    }
}
