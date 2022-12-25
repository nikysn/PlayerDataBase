
using PlayerDataBase.CLI;

Database database = new Database();
//database.CreatePlayers();
bool isWorking = true;

while (isWorking)
{
    Console.Clear();
    Console.WriteLine("Введите номер команды :\n" +
        "1. Добавить игрока\n" +
        "2. Показать список игроков\n" +
        "3. Удалить игрока\n" +
        "4. Забанить игрока\n" +
        "5. Разбанить игрока\n" +
        "6. Выход");

    string commandNumber = Console.ReadLine();

    switch (commandNumber)
    {
        case "1":
            database.AddPlayer();
            break;
        case "2":
             database.Display();
            break;
        case "3":
            // database.DeletePlayer();
            break;
        case "4":
            // database.BanPlayer();
            break;
        case "5":
            // database.UnbanPlayer();
            break;
        case "6":
            isWorking = false;
            break;
    }
    Console.ReadLine();
    Console.Clear();
}
