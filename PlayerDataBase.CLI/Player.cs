namespace PlayerDataBase.CLI;

public class Player
{
    public Player(int id, string name, int level, string banStatus = "not bannet" )
    {
        Name = name;
        Level = level;
        Id = id;
        BanStatus = banStatus;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Level { get; private set; }
    public string BanStatus { get; private set; }
   /* public bool IsBanned {get; private set;}
     public string BanStatus
     {
         get
         {
             return _ = IsBanned == true ? "banned" : "not banned";
         }
     }
*/

    //public void Ban() => IsBanned = true;

    public void Ban() => BanStatus = "banned";

    public void Unban() => BanStatus = "not banned";

    public override string ToString()
    {
        return $"Id - {Id}. Ник - {Name}. Уровень - {Level}. Статус бана: {BanStatus}";
    }
}