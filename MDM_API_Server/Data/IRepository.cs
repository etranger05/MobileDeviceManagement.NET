
using System.Collections.Generic;
using Rest.DTOs;
using Rest.Models;

namespace Rest.Data
{
    public interface IRepository
    {
        bool SaveChanges();
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User GetUserByName(string name);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}