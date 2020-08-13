
using AutoMapper;
using Rest.DTOs;
using Rest.Models;

namespace Rest.Profiles
{
    public class PodcastFeedProfile : Profile
    {
        public PodcastFeedProfile()
        {
            CreateMap<PodcastFeed, PodcastFeedReadDTO>();
            CreateMap<PodcastFeedCreateDTO, PodcastFeed>();
            CreateMap<PodcastFeedUpdateDTO, PodcastFeed>();
            CreateMap<PodcastFeed, PodcastFeedUpdateDTO>();
        }
    }
}