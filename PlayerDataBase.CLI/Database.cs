using System.Text.Json;

namespace PlayerDataBase.CLI;

public class Database
{
    private List<Player> _players = new List<Player>();
    private const string DirectoryName = "./dataBase/";

    public void CreatePlayers()
    {
        _players.Add(new Player("Vadim", 2));
        _players.Add(new Player("Amir", 10));
    }
    private List<Player> Get()
    {
        var files = Directory.GetFiles(DirectoryName);
        var players = new List<Player>();

        foreach (var file in files)
        {
            var json = File.ReadAllText(file);
            var player = JsonSerializer.Deserialize<Player>(json);

            if(player == null)
            {
                throw new Exception("Player cannot be deserialized");
            }
            players.Add(player);
        }
        return players;
    }

    public bool AddPlayer()
    {
        Console.Write("Введите ник игрока : ");
        string name = Console.ReadLine();
        Console.Write("Введите уровень игрока : ");
        int level;
        bool getNumber = int.TryParse(Console.ReadLine(), out level);

        if (getNumber)
        {
            Directory.CreateDirectory(DirectoryName);
            _players.Add(new Player(name, level));
            var json = JsonSerializer.Serialize(_players.Last(), new JsonSerializerOptions { WriteIndented = true });
            var fileName = $"{Guid.NewGuid()}.json";
            var filePath = Path.Combine(DirectoryName, fileName);
            File.WriteAllText(filePath, json);

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

        for (int i = 0; i < Get().Count; i++)
        {
            Get()[i].Display();
        }
    }

    public void DeletePlayer()
    {
        _players.RemoveAt(GetPlayerNumber("которого хотите удалить:") - 1);
    }

    public int GetPlayerNumber(string command)
    {
        int playerNumber = 0;
        bool getNumber = false;

        while (getNumber != true)
        {
            Display();
            Console.WriteLine($"Введите порядковый номер игрока {command}");
            string input = Console.ReadLine();
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