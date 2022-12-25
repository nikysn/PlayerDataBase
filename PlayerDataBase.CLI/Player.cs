namespace PlayerDataBase.CLI;
    
public class Player
{

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Level { get; private set; }
    public bool IsBanned { get; private set; } = false;
    public string BanStatus
    {
        get
        {
            return _ = IsBanned == true ? "Забанен" : "Не забанен";
        }
    }

    public Player(string name, int level)
    {
        Name = name;
        Level = level;
    }

    public void Ban() => IsBanned = true;

    public void Unban() => IsBanned = false;

    public void Display() => Console.WriteLine($"Порядковый номер - {Id}. Ник - {Name}. Уровень - {Level}. {BanStatus}");
}