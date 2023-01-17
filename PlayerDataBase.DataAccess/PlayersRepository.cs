using System.Text.Json;
using PlayerDataBase.DataAccess;

namespace PlayerDataBase.DataAccess;

public static class PlayersRepository
{
    private const string DirectoryName = "./dataBase/";
    public static Guid DBUpdate(Player player, Guid playerId)
    {
        Directory.CreateDirectory(DirectoryName);
        var json = JsonSerializer.Serialize(player with { Id = playerId }, new JsonSerializerOptions { WriteIndented = true });

        string FileName = $"{playerId}.json";
        string FilePath = Path.Combine(DirectoryName, FileName);
        File.WriteAllText(FilePath, json);

        Console.WriteLine("Operation completed");
        return playerId;
    }

    public static Player[] GetPlayers()
    {
        var filePath = Path.Combine(DirectoryName);
        var files = Directory.GetFiles(filePath);

        List<Player> players = new List<Player>();

        foreach (var file in files)
        {
            var json = File.ReadAllText(file);
            var player = JsonSerializer.Deserialize<Player>(json);
            if (player == null) throw new Exception("Player cannot be deserialized");
            players.Add(player);
        }
        return players.ToArray();
    }

    public static void DeletePlayer(int number)
    {
        List<Player> players = GetPlayers().ToList();

        string FilePath = Path.Combine(DirectoryName, $"{players[number - 1].Id}.json");
        File.Delete(FilePath);
    }

    public static Guid AddPlayer(string name, int level)
    {
        Player newPlayer = new Player(name, level);
        var PlayerId = Guid.NewGuid();
        DBUpdate(newPlayer,PlayerId);

        return PlayerId;
    }

    public static void ChangeBanStatus(int number, string action)
    {
        List<Player> players = GetPlayers().ToList();
        Player player = players[number - 1];

        if (action == "ban") player.Ban();
        else if (action == "unban") player.UnBan();

        DBUpdate(player,player.Id);
    }
}


