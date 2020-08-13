
using System.ComponentModel.DataAnnotations;

namespace Rest.Models
{
    public class PodcastFeed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2048)]
        public string FeedUrl { get; set; }
        
        // Foreign-Key
        public int UserId { get; set; }
    }
}