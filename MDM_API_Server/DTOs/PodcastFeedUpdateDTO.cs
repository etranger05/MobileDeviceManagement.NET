
using System.ComponentModel.DataAnnotations;

namespace Rest.DTOs
{
    public class PodcastFeedUpdateDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2048)]
        public string FeedUrl { get; set; }
    }
}