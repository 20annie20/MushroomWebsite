using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MushroomWebsite.Models
{
    public class Mushroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1024)]
        public string Name { get; set; }

        public string Description { get; set; }
        public bool IsPoisonous { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        public ICollection<EntryMushroom> EntryMushrooms { get; set; }
    }
}
