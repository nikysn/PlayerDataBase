namespace PlayerDataBase.CLI;

public class Player
{
    public Player(int id, string name, int level, string banStatus = "not bannet" )
    {
        Name = name;
        Level = level;
        Id = id;
        BanStatus = banStatus;
    }

    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public string BanStatus { get; protected set; }

     public override string ToString()
    {
        return $"Id - {Id}. Ник - {Name}. Уровень - {Level}. Статус бана: {BanStatus}";
    }

    public void Ban() => BanStatus = "banned";

    public void Unban() => BanStatus = "not banned";
}
