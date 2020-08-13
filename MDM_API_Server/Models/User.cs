

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Rest.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [FromQuery(Name = "name")]
        public string Username { get; set; }

        public ICollection<PodcastFeed> Podcasts { get; set; }
    }
}