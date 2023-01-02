using System.Text.Json;

namespace PlayerDataBase.CLI;

public class Database
{
    private const string DirectoryName = "./dataBase/";
    private const string FileName = "db.json";
    private const string DBFilePath = DirectoryName + FileName;

    private int CreateId()
    {
        int id = 1;
        var players = GetPlayers();

        for (int i = 1; i <= players.Length; i++)
        {
            for (int j = 0; j < players.Length; j++)
            {
                if (id == players[j].Id)
                {
                    id++;
                    i--;
                }
            }
        }
        return players.Length == 0 ? 1 : id;
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

        var newPlayer = new Player(CreateId(), name, level);
        List<Player> players = GetPlayers().ToList();
        players.Add(newPlayer);

        var json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(DBFilePath, json);
    }

    public void Display()
    {
        Console.WriteLine("List of players:");
        var players = GetPlayers();

        for (int i = 0; i < players.Length; i++)
        {
            Console.WriteLine(players[i]);
        }
    }

    public void DeletePlayer()
    {
        int playerId = GetPlayerId();
        List<Player> players = GetPlayers().ToList();
        Player playerForDelete = players.FirstOrDefault(u => u.Id == playerId);

        if (playerForDelete != null)
        {
            players.Remove(playerForDelete);
            var json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DBFilePath, json);
        }
    }

    public int GetPlayerId()
    {
        int playerId;
        bool isPlayerId;

        do
        {
            Display();
            Console.WriteLine($"Enter player id:");
            string? input = Console.ReadLine();
            isPlayerId = int.TryParse(input, out playerId);
            if (!isPlayerId) Console.WriteLine("you entered incorrect data");
        } while (!isPlayerId);

        return playerId;
    }

    public void ChangeBanStatus(string commandNumber)
    {
        int playerId = GetPlayerId();

        List<Player> players = GetPlayers().Cast<Player>().ToList();
        Player playerForBanned = players.FirstOrDefault(u => u.Id == playerId);

        if (playerForBanned != null)
        {
            if(commandNumber == "4") playerForBanned.Ban();
            if(commandNumber == "5") playerForBanned.Unban();
            var json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DBFilePath, json);
            Console.WriteLine("Operation completed");
        }
    }

}