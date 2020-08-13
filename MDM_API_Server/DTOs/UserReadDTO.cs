
using System.Collections.Generic;

namespace Rest.DTOs
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<PodcastFeedReadDTO> Podcasts { get; set; }
    }
}