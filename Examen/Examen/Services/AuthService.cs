using Examen.Data;
using Examen.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Examen.Services
{
    public static class AuthService
    {
        // This holds the current logged in user
        // If it's null, no one is logged in
        public static User? CurrentUser { get; private set; } = null;

        // LOGIN — finds the user in the database by username and password
        public static bool Login(string username, string password)
        {
            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);

            if (user != null)
            {
                CurrentUser = user;
                return true; // login success
            }

            return false; // login failed
        }

        // LOGOUT — just clears the current user
        public static void Logout()
        {
            CurrentUser = null;
        }

        // REGISTER — adds a new user to the database
        public static bool Register(string name, string username, string email, string password)
        {
            using var db = new AppDbContext();

            // Check if username already exists
            bool exists = db.Users.Any(u => u.Username == username);
            if (exists) return false;

            db.Users.Add(new User
            {
                Name = name,
                Username = username,
                Email = email,
                Password = password
            });

            db.SaveChanges();
            return true;
        }
    }
}
