using Pcf.Administration.Core.Abstractions.Services;
using Pcf.Core.Integration;

namespace Pcf.Administration.Integration;

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

        if (dto.PartnerManagerId.HasValue)
            await promocodeService.UpdateAppliedPromocodesAsync(dto.PartnerManagerId.Value);
        else
            error = "PartnerManagerId is null";

        return error;

    }

    protected override Task<string> OnTest(string message)
    {
        var error = string.Empty;
        Console.WriteLine($"Test message: {message}");

        return Task.FromResult(error);
    }
}
