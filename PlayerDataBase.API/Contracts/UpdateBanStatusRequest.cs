using System.ComponentModel;
using PlayerDataBase.DataAccess;

namespace PlayerDataBase.API.Contracts;

public class UpdateBanStatusRequest
{
    [Description("Accepted values are 'ban' or 'unban'")]
    public BanStatuses Action {get;set;}
    public Guid PlayerId {get;set;}
}
