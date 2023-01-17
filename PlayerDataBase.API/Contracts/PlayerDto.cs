using System.Text.Json.Serialization;
using PlayerDataBase.DataAccess;

namespace PlayerDataBase.API.Contracts
{
    public sealed class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        [JsonConverter(typeof(BanStatusConverter))]
        public BanStatuses BanStatus { get; set; }

        [JsonConverter(typeof(BanStatusConverter))]
        public enum BanStatuses : byte
        {
            NotBanned,
            Banned
        }
    }
}