namespace PlayerDataBase.CLI;

public class Player
{

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Level { get; private set; }
    public bool IsBanned { get; private set; } = false;
    public string BanStatus
    {
        get
        {
            return _ = IsBanned == true ? "banned" : "not banned";
        }
    }

    public Player(int id,string name, int level)
    {
        Name = name;
        Level = level;
        Id = id;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public void Ban() => IsBanned = true;

    public void Unban() => IsBanned = false;

   /* public override string ToString()
    {
        return $"{Id} {Name} {Level} {BanStatus}";

    }*/

    public void Display() => Console.WriteLine($"Id - {Id}. Ник - {Name}. Уровень - {Level}. {BanStatus}");
}