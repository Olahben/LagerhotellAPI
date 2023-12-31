﻿using Newtonsoft.Json;

namespace LagerhotellAPI.Models
{
    public class UserRepository
    {
        private List<User> _users;
        private readonly string _filePath = @"C:\Users\ohage\SKOLE\Programmering\Lagerhotell\wwwroot\Data\users.json";
        public List<User> Users
        {
            get
            {
                if (_users == null)
                {
                    Load();
                }
                return _users;
            }
        }

        public User Add(string firstName, string lastName, string phoneNumber, string birthDate, string password)
        {
            var id = Guid.NewGuid().ToString();
            User user = new User(id, firstName, lastName, phoneNumber, birthDate, password);
            Users.Add(user);
            // Ensure JSON is saved
            Save();
            return user;

        }

        public User? Get(string phoneNumber)
        {
            return Users.Where(_ => _.PhoneNumber == phoneNumber).FirstOrDefault();
        }
        private void Save()
        {
            var updatedJson = JsonConvert.SerializeObject(Users);
            System.IO.File.WriteAllText(_filePath, updatedJson);
        }
        private void Load()
        {
            // Check if JSON is read
            var existingJson = System.IO.File.ReadAllText(_filePath);
            _users = JsonConvert.DeserializeObject<List<User>>(existingJson);
        }

        public string Password(string phoneNumber)
        {
            var user = Get(phoneNumber);
            // Handle if user is null
            if (user == null)
            {
                return null;
            }
            return user.Password;
        }

        public User? GetUserById(string Id)
        {
            User user = Users.Where(_ => _.Id == Id).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
