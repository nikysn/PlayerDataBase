namespace PlayerDataBase.API.Contracts
{
    public sealed class GetPlayerResponse
    {
        public PlayerDto[] Players { get; set; }
    }
}