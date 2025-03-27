using System.Collections.Generic;
using System.Linq;

public class FakeAuthService
{
    public const string ApiKey = "my-static-api-key"; // Static API key
    private static readonly List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "Seyma", Password = "1234" }, // First user
        new User { Id = 2, Username = "user2", Password = "password2" } // Second user
    };

    public User Login(string username, string password)
    {
        // Find and return the user that matches the provided username and password
        return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
}
