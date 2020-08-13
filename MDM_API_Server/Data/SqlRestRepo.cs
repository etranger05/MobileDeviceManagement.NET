
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rest.DTOs;
using Rest.Models;

namespace Rest.Data
{
    public class SqlRestRepo : IRepository
    {
        private readonly RestContext context;
        public SqlRestRepo(RestContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            context.Users.Remove(user);
        }

        public User GetUserById(int id)
        {
            return context.Users
                .Include(user => user.Podcasts)
                .AsNoTracking()
                .FirstOrDefault(user => user.Id == id);
        }

        public User GetUserByName(string name)
        {
            return context.Users
                .Include(user => user.Podcasts)
                .AsNoTracking()
                .FirstOrDefault(user => user.Username == name);
        }

        public void UpdateUser(User user)
        {
            if (context.PodcastFeed.Any()) {
                List<PodcastFeed> removals = context.PodcastFeed.Where(p => p.UserId == user.Id).ToList();

                foreach (PodcastFeed feed in user.Podcasts)
                {
                    var feedToUpdate = context.PodcastFeed.FirstOrDefault(p => p.Id == feed.Id);
                    if (feedToUpdate != null) // Update an existing podcast feed
                    {
                        feedToUpdate.FeedUrl = feed.FeedUrl;
                        removals.Remove(feedToUpdate);
                    } else { // Add a new podcast feed
                        feed.UserId = user.Id;
                        context.PodcastFeed.Add(feed);
                    }
                }

                context.PodcastFeed.RemoveRange(removals); // Remove anything that used to be tracked, but was not provided in the update
            }
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users
                .Include(user => user.Podcasts)
                .AsNoTracking()
                .ToList();
        }
    }
}