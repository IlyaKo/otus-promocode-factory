using Grpc.Core;

namespace Pcf.Core.Integration;

public abstract class ExchangeServer : ExchangeService.ExchangeServiceBase
{
    public override async Task<Response> PromocodeCreated(Promocode request, ServerCallContext context)
    {
        var error = string.Empty;
        try
        {
            var dto = new PromocodeDto
            {
                Code = request.Code,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Parse(request.BeginDate),
                EndDate = DateTime.Parse(request.EndDate),
                PartnerId = Guid.Parse(request.PartnerId),
                PartnerManagerId = string.IsNullOrEmpty(request.PartnerManagerId) ? null : Guid.Parse(request.PartnerManagerId),
                PreferenceId = Guid.Parse(request.PreferenceId)
            };
            error = await OnPromocodeCreated(dto);
        }
        catch (Exception ex)
        {
            error = ex.Message;           
        }

        return new()
        {
            ErrorMessage = error,
            Success = string.IsNullOrEmpty(error)
        };
    }

    protected abstract Task<string> OnPromocodeCreated(PromocodeDto dto);

    public override async Task<Response> Test(Message request, ServerCallContext context)
    {
        var error = await OnTest(request.Text);

        return new()
        {
            ErrorMessage = error,
            Success = string.IsNullOrEmpty(error),
        };
    }

    protected abstract Task<string> OnTest(string message);
}
