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
         
        public Article Article { get; set; }

        public ICollection<EntryMushroom> EntryMushrooms { get; set; }

    }

    public class EntryMushroom
    {
        public int EntryId { get; set; }
        public Entry Entry { get; set; }
        public int MushroomId { get; set; }
        public Mushroom Mushroom { get; set; }
    }
}
