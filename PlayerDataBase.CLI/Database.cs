using System.Text.Json;

namespace PlayerDataBase.CLI;

public class Database
{
    private const string DirectoryName = "./dataBase/";

    private void DBUpdate(string FileName, Player player)
    {
        string FilePath = Path.Combine(DirectoryName, FileName);
        var json = JsonSerializer.Serialize(player, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);

        Console.WriteLine("Operation completed");
    }

    private int GetPlayerNumber()
    {
        int playerNumber;
        bool isPlayerNumber;

        do
        {
            Display(GetPlayers());
            Console.WriteLine($"Enter the player's serial number:");
            string? input = Console.ReadLine();
            isPlayerNumber = int.TryParse(input, out playerNumber);
            if (!isPlayerNumber) Console.WriteLine("you entered incorrect data");
        } while (!isPlayerNumber);

        return playerNumber;
    }

    public Player[] GetPlayers()
    {
        var files = Directory.GetFiles(DirectoryName);

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

    public void AddPlayer()
    {
        Console.Write("Enter player name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter player level: ");
        bool isLevel = int.TryParse(Console.ReadLine(), out int level);
        if (!isLevel)
        {
            Console.WriteLine("You entered incorrect data");
            return;
        }
        var newPlayer = new Player($"{Guid.NewGuid()}", name, level);

        string FileName = $"{newPlayer.Id}.json";
        DBUpdate(FileName,newPlayer);
    }

    public void Display(Player[] players)
    {
        Console.WriteLine("List of players:");

        for (int i = 0; i < players.Length; i++)
        {
            Console.WriteLine($"{i + 1}.{players[i]}");
        }
    }

    public void DeletePlayer()
    {
        int playerNumber = GetPlayerNumber() - 1;
        List<Player> players = GetPlayers().ToList();

        string FilePath = Path.Combine(DirectoryName, $"{players[playerNumber].Id}.json");
        File.Delete(FilePath);
    }

    public void ChangeBanStatus(string commandNumber)
    {
        int playerNumber = GetPlayerNumber() - 1;
        List<Player> players = GetPlayers().ToList();

        if (commandNumber == "4") players[playerNumber].Ban();
        if (commandNumber == "5") players[playerNumber].UnBan();

        string FileName = $"{players[playerNumber].Id}.json";
        DBUpdate(FileName,players[playerNumber]);
    }
}