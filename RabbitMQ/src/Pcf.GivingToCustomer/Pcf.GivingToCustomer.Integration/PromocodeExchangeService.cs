using Pcf.Core.Integration;
using Pcf.GivingToCustomer.Core.Abstractions.Services;
using System;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Integration;

public sealed class PromocodeExchangeService : ExchangeServer
{
    private readonly IPromocodeService promocodeService;

    public PromocodeExchangeService(IPromocodeService promocodeService)
    {
        this.promocodeService = promocodeService;
    }

    protected override async Task<string> OnPromocodeCreated(PromocodeDto dto)
    {
        var error = string.Empty;

        try
        {
            await promocodeService.GivePromoCodesToCustomersWithPreference(dto);
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
