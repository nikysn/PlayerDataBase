using System.Text.Json.Serialization;

namespace PlayerDataBase.CLI;

public class Player
{

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Level { get; private set; }
    //public string BanStatus { get; private set; }
    public bool IsBanned {get; private set;}
     public string BanStatus
     {
         get
         {
             return _ = IsBanned == true ? "banned" : "not banned";
         }
     }

    public Player(int id, string name, int level)
    {
        Name = name;
        Level = level;
        Id = id;
    }

    public void Ban() => IsBanned = true;

    //public void Ban() => BanStatus = "banned";

    // public void Unban() => IsBanned = false;

    public void Display() => Console.WriteLine($"Id - {Id}. Ник - {Name}. Уровень - {Level}. Статус бана: {BanStatus}");
}