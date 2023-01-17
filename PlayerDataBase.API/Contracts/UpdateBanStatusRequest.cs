using System.ComponentModel;

namespace PlayerDataBase.API.Contracts;

public class UpdateBanStatusRequest
{
    [Description("Accepted values are 'ban' or 'unban'")]
    public string action {get;set;}
    public int number {get;set;}
}
