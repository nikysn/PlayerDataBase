namespace PlayerDataBase.CLI;

public class Player
{
    public Player(string id,string name, int level, BanStatuses banStatus = BanStatuses.NotBanned)
    {
        Name = name;
        Level = level;
        Id = id;
        BanStatus = banStatus;
    }
    public string Id { get; protected set; }
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public BanStatuses BanStatus { get; protected set; }

    public enum BanStatuses : byte
    {
        NotBanned,
        Banned
    }

    public override string ToString()
    {
        return $"Id - {Id}. Ник - {Name}. Уровень - {Level}. Статус бана: {BanStatus}";
    }

    public void Ban() => BanStatus = BanStatuses.Banned;

    public void UnBan() => BanStatus = BanStatuses.NotBanned;
}
