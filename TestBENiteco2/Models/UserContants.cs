namespace TestBENiteco2.Models;

public class UserContants
{
    public static List<User> Users = new()
    {
        new User(){ Username="huynq",Password="abcd@123",Role="Admin"},
        new User(){ Username="khanhkv",Password="abcd@123",Role="Staff"}
    };
}