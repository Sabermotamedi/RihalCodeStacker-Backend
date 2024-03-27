using Rihal.ReelRise.Infrastructure.Identity;

namespace Rihal.ReelRise.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
