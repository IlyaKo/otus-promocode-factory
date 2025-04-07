using Microsoft.AspNetCore.SignalR;
using Pcf.Core.Integration;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Integration;

public sealed class PromocodeHub : Hub
{
    public async Task PromocodeCreated(PromocodeDto promocode)
    {
        await Clients.All.SendAsync("PromocodeReceived", promocode);
    }
}
