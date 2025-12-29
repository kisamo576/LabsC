namespace LAB5;

public class AuthService
{
    private List<User> users = new();
    private const string UsersFile = "users.txt";
    
    public AuthService()
    {
        LoadUsers();
    }
    
    private void LoadUsers()
    {
        if (!File.Exists(UsersFile))
        {
            users.Add(new User("admin", "admin123", "admin"));
            users.Add(new User("user", "user123", "user"));
            SaveUsers();
            return;
        }
        
        var lines = File.ReadAllLines(UsersFile);
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length == 3)
            {
                users.Add(new User(parts[0], parts[1], parts[2]));
            }
        }
    }
    
    private void SaveUsers()
    {
        var lines = new List<string>();
        foreach (var user in users)
        {
            lines.Add($"{user.Login}:{user.Password}:{user.Role}");
        }
        File.WriteAllLines(UsersFile, lines);
    }
    
    public User? Login(string login, string password)
    {
        return users.FirstOrDefault(u => 
            u.Login == login && u.Password == password);
    }
    
    public bool Register(string login, string password)
    {
        if (users.Any(u => u.Login == login))
            return false;
            
        users.Add(new User(login, password, "user"));
        SaveUsers();
        return true;
    }
}