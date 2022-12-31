using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;

namespace PlayerDataBase.CLI;

public class Database
{
    private const string DirectoryName = "./dataBase/";
    static string FileName { get; set; } = "db.json";
    string DBFilePath { get; set; } = DirectoryName + FileName;
    //string filePath = Path.Combine(DirectoryName, FileName);

    private int CreateId()
    {
        int id = 1;
        for (int i = 1; i <= GetPlayers().Count; i++)
        {
            for (int j = 0; j < GetPlayers().Count; j++)
            {
                if (id == GetPlayers()[j].Id)
                {
                    id++;
                    i--;
                }
            }
        }
        return GetPlayers().Count == 0 ? 1 : id;
    }

    public List<Player> GetPlayers()
    {
        if (File.Exists(DBFilePath) == false)
        {
            var file = File.Create(DBFilePath);
            file.Close();
        }

        var json = File.ReadAllText(DBFilePath);
        //List<Player> currentPlayers = JsonConvert.DeserializeObject<List<Player>>(json);
        List<Player> curPlayers = JsonSerializer.Deserialize<List<Player>>(json);

        return curPlayers ?? new List<Player>();
    }

    public void AddPlayer()
    {
        Console.Write("Введите ник игрока : ");
        string? name = Console.ReadLine();
        Console.Write("Введите уровень игрока : ");
        int level;
        bool getNumber = int.TryParse(Console.ReadLine(), out level);

        if (getNumber)
        {
            List<Player> allCurrentPlayers = GetPlayers();
            allCurrentPlayers.Add(new Player(CreateId(), name, level));

            var json = System.Text.Json.JsonSerializer.Serialize(allCurrentPlayers, new JsonSerializerOptions { WriteIndented = true });
            //var json = JsonConvert.SerializeObject(allCurrentPlayers);
            //var fileName = $"{Guid.NewGuid()}.json";
            File.WriteAllText(DBFilePath, json);
        }
        else
        {
            Console.WriteLine("Вы ввели не коректные данные");
        }
    }

    public void Display()
    {
        Console.WriteLine("Список игроков:");

        for (int i = 0; i < GetPlayers().Count; i++)
        {
            GetPlayers()[i].Display();
        }
    }

    public void DeletePlayer()
    {
        int playerId = GetPlayerNumber("которого нужно удалить: ");
        List<Player> allCurrentPlayers = GetPlayers();
        Player playerForDelete = allCurrentPlayers.FirstOrDefault(u => u.Id == playerId);

        if (playerForDelete != null)
        {
            allCurrentPlayers.Remove(playerForDelete);
            var json = System.Text.Json.JsonSerializer.Serialize(allCurrentPlayers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DBFilePath, json);
        }
    }

    public int GetPlayerNumber(string command)
    {
        int playerNumber = 0;
        bool getNumber = false;

        while (getNumber != true)
        {
            Display();
            Console.WriteLine($"Введите ID игрока {command}");
            string? input = Console.ReadLine();
            getNumber = int.TryParse(input, out playerNumber);
        }

        return playerNumber;
    }

    public void BanPlayer()
    {
        int playerId = GetPlayerNumber("которого нужно забанить: ");
        List<Player> allCurrentPlayers = GetPlayers();
        Player playerForBanned = allCurrentPlayers.FirstOrDefault(u => u.Id == playerId);

        if (playerForBanned != null)
        {
            playerForBanned.Ban();
            var json = System.Text.Json.JsonSerializer.Serialize(allCurrentPlayers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DBFilePath, json);
        }
        //_players[GetPlayerNumber("которого нужно забанить") - 1].Ban();
    }

    /*public void UnbanPlayer()
    {
        _players[GetPlayerNumber("которого нужно разбанить") - 1].Unban();
    }*/
}