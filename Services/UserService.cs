﻿using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainBookingBackend.Models;
using TrainBookingBackend.Models.Interfaces;
using TrainBookingBackend.Services.Interfaces;

namespace TrainBookingBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IUserDBSettings userDBSettings, IMongoClient mongoClient) {
            var databaseName = userDBSettings.UserDatabaseName;
            var collectionName = userDBSettings.UserCollectionName;

            var database = mongoClient.GetDatabase(databaseName);
            _users = database.GetCollection<User>(collectionName);
        }
        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public User? LoginUser(string email, string password)
        {
            User existingUser = _users.Find(user => user.Email == email).FirstOrDefault<User>();

            if (existingUser == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, existingUser.Password))
            {
                return existingUser;
            }

            return null;
        }

        public void DeleteUser(string id)
        {
            _users.DeleteOne(id);
        }

        public User GetUser(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault<User>();
        }

        public List<User> GetUsers()
        {
            return _users.Find<User>(user => true).ToList<User>();
        }

        public User UpdateUser(string id, User user)
        {
            _users.ReplaceOne(user => user.Id == id, user);
            return _users.Find<User>(user => user.Id == id).FirstOrDefault();
        }
    }
}
