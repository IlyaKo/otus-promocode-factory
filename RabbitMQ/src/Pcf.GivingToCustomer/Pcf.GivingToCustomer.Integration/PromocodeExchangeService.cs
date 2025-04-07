using Microsoft.AspNetCore.SignalR;
using Pcf.Core.Integration;
using Pcf.GivingToCustomer.Core.Abstractions.Services;
using System;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Integration;

public sealed class PromocodeExchangeService : ExchangeServer
{
    private readonly IPromocodeService promocodeService;
    private readonly IHubContext<PromocodeHub> hub;

    public PromocodeExchangeService(IPromocodeService promocodeService,
        IHubContext<PromocodeHub> hub)
    {
        this.promocodeService = promocodeService;
        this.hub = hub;
    }

    protected override async Task<string> OnPromocodeCreated(PromocodeDto dto)
    {
        var error = string.Empty;

        try
        {
            await promocodeService.GivePromoCodesToCustomersWithPreference(dto);
            await hub.Clients.All.SendAsync("PromocodeReceived", dto);
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }

        return error;
    }

    protected override Task<string> OnTest(string message)
    {
        var error = string.Empty;
        Console.WriteLine($"Test message: {message}");

        return Task.FromResult(error);
    }
}
