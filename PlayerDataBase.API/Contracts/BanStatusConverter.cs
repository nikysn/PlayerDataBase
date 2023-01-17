using System.Text.Json;
using System.Text.Json.Serialization;
using static PlayerDataBase.API.Contracts.PlayerDto;

namespace PlayerDataBase.DataAccess;

public class BanStatusConverter : JsonConverter<BanStatuses>
{
   public override BanStatuses Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (BanStatuses)Enum.Parse(typeof(BanStatuses), reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, BanStatuses value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}


