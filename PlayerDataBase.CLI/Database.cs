using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PlayerDataBase.CLI;

public class Database
{
    private List<Player> _players = new List<Player>();
    private const string DirectoryName = "./dataBase/";
    static string FileName { get; set; } = "db.json";
    string DBFilePath { get; set; } = DirectoryName + FileName;
    //string filePath = Path.Combine(DirectoryName, fileName);

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

    private void SaveToDB(List<Player> players)
    {
        //string serializedUsers = JsonConvert.SerializeObject(players, new JsonSerializerOptions{WriteIndented = true});
        // File.WriteAllText()

    }

    public List<Player> GetPlayers()
    {
        if(File.Exists(DBFilePath) == false)
        {
            var file = File.Create(DBFilePath);
            file.Close();
        }

        string json = File.ReadAllText(DBFilePath);
        List<Player> currentPlayers = JsonConvert.DeserializeObject<List<Player>>(json);
        return currentPlayers ?? new List<Player>();
    }

    public bool AddPlayer()
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

            return true;
        }
        else
        {
            Console.WriteLine("Вы ввели не коректные данные");
            return false;
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

        if(playerForDelete != null)
        {
            allCurrentPlayers.Remove(playerForDelete);
            var json = System.Text.Json.JsonSerializer.Serialize(allCurrentPlayers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DBFilePath, json);
        }


        // var serializedUsers = JsonConvert.
        //  var json = JsonSerializer.Serialize(allCurrentPlayers.Last(), new JsonSerializerOptions { WriteIndented = true });
        // _players.RemoveAt(GetPlayerNumber("которого хотите удалить:") - 1);

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
        _players[GetPlayerNumber("которого нужно забанить") - 1].Ban();
    }

    public void UnbanPlayer()
    {
        _players[GetPlayerNumber("которого нужно разбанить") - 1].Unban();
    }
}