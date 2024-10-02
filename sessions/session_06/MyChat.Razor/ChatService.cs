using Microsoft.EntityFrameworkCore;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
}

class Message
{
    int MessageId { get; set; }
    int UserId { get; set; }
    string Text { get; set; }
    User User { get; set; }
}

class User
{
    int UserId { get; set; }
    string Name { get; set; }
    ICollection<Message> Messages { get; set; }
}

class ChatDBContext : DbContext
{
    DbSet<Message> messages { get; set; }
    DbSet<User> users { get; set; }
    
    public ChatDBContext(DbContextOptions<ChatDBContext> options) : base(options)
    {
        
    }
}

public class ChatService : ICheepService
{
    // These would normally be loaded from a database for example
    private static readonly List<CheepViewModel> _cheeps = new()
        {
            new CheepViewModel("Helge", "Hello, BDSA students!", UnixTimeStampToDateTimeString(1690892208)),
            new CheepViewModel("Adrian", "Hej, velkommen til kurset.", UnixTimeStampToDateTimeString(1690895308)),
        };

    public List<CheepViewModel> GetCheeps()
    {
        return _cheeps;
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        return _cheeps.Where(x => x.Author == author).ToList();
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

}
