namespace PlayerDataBase.CLI;

public class Player
{
    public Player(string name, int level, BanStatuses banStatus = BanStatuses.NotBanned)
    {
        Name = name;
        Level = level;
        Id = Guid.NewGuid();
        BanStatus = banStatus;
    }
    public Guid Id { get; protected set; }
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
