namespace PlayerDataBase.DataAccess;

public record Player
{
    public Player(string name, int level, BanStatuses banStatus = BanStatuses.NotBanned)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("CreateDate cannot be null or whitespace",paramName: nameof(level));
        }
        Name = name;
        Level = level;
        BanStatus = banStatus;
    }
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Level { get; init; }
    public BanStatuses BanStatus { get; protected set; }

    

    public override string ToString()
    {
        return $"Id - {Id}. Ник - {Name}. Уровень - {Level}. Статус бана: {BanStatus}";
    }

    public void Ban() => BanStatus = BanStatuses.Banned;

    public void UnBan() => BanStatus = BanStatuses.NotBanned;
}
