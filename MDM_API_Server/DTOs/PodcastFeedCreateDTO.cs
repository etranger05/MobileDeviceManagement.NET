
using System.ComponentModel.DataAnnotations;

namespace Rest.DTOs
{
    public class PodcastFeedCreateDTO
    {
        [Required]
        [MaxLength(2048)]
        public string FeedUrl { get; set; }
    }
}