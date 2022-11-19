using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MushroomWebsite.Models
{
    public class Mushroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Name { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }
        public bool IsPoisonous { get; set; }
    }
}
