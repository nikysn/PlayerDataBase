using System.Text.Json;

namespace PlayerDataBase.CLI;

public class Database
{
    private const string DirectoryName = "./dataBase/";
    private const string FileName = "db.json";
    private const string DBFilePath = DirectoryName + FileName;

    private void DBUpdate(List<Player> players)
    {
        var json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(DBFilePath, json);
        Console.WriteLine("Operation completed");
    }

    private int GetPlayerNumber()
    {
        int playerId;
        bool isPlayerId;

        do
        {
            Display(GetPlayers());
            Console.WriteLine($"Enter the player's serial number:");
            string? input = Console.ReadLine();
            isPlayerId = int.TryParse(input, out playerId);
            if (!isPlayerId) Console.WriteLine("you entered incorrect data");
        } while (!isPlayerId);

        return playerId;
    }

    public Player[] GetPlayers()
    {

        Player[] emptyArray = Array.Empty<Player>();

        if (!File.Exists(DBFilePath))
        {
            return emptyArray;
        }
        var json = File.ReadAllText(DBFilePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return emptyArray;
        }

        var players = JsonSerializer.Deserialize<Player[]>(json);


        return players ?? emptyArray;
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

        var newPlayer = new Player(name, level);
        List<Player> players = GetPlayers().ToList();
        players.Add(newPlayer);

        DBUpdate(players);
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

        players.Remove(players[playerNumber]);

        DBUpdate(players);
    }

    public void ChangeBanStatus(string commandNumber)
    {
        int playerNumber = GetPlayerNumber() - 1;
        List<Player> players = GetPlayers().Cast<Player>().ToList();

        if (commandNumber == "4") players[playerNumber].Ban();
        if (commandNumber == "5") players[playerNumber].UnBan();

        DBUpdate(players);
    }
}