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
        //TODO add Article binding, Author binding, Mushrooms 
    }
}
