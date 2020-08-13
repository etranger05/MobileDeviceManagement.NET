

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Rest.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        [MaxLength(255)]
        [FromQuery(Name = "name")]
        public string Username { get; set; }

        public ICollection<PodcastFeedCreateDTO> Podcasts { get; set; }
    }
}