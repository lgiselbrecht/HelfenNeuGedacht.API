using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class DashboardHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        
        var organizationId = Context.User?.FindFirst("OrganizationId")?.Value;

        if (!string.IsNullOrEmpty(organizationId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"organization-{organizationId}");
        }

        await base.OnConnectedAsync();
    }
}