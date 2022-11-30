using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MushroomWebsite.Models
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
         
        public Article Article { get; set; } //moze to tez binded po id

        public ICollection<Mushroom> Mushrooms { get; set; }

    }
}
