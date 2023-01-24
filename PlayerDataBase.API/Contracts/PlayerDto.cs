using System.Text.Json.Serialization;
using PlayerDataBase.DataAccess;

namespace PlayerDataBase.API.Contracts
{
    public sealed partial class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string BanStatus { get; set; }
    }
}