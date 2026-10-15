using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.GtfsValidator;
using Byd.GtfsManagement.Interfaces.Feeds;

namespace Byd.GtfsManagement;

/// <summary>
/// GTFS dogrulayicisinin servis kayitlari.
/// </summary>
// islevi: Kanonik dogrulayiciyi HTTP client'i ile kaydeder.
// sistemdeki gorevi: Application modulu dogrulayici kablolamasini tek satirla cagirir.
public static class GtfsFeedConfigurationExtensions
{
    // Yerel dogrulayiciyi yonlendirme izlemeyen, uzun sureli client ile kaydeder.
    public static IServiceCollection AddGtfsFeedIntegrations(this IServiceCollection services)
    {
        services.AddTransient<IGtfsFeedValidator, GtfsFeedValidator>();
        services.AddHttpClient(GtfsValidationConsts.HttpClientName, client =>
                client.Timeout = GtfsValidationConsts.RequestTimeout)
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false })
            .RemoveAllLoggers();
        return services;
    }
}
