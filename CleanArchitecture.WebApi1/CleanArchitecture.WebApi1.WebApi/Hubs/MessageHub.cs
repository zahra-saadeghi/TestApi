using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi1.WebApi.Hubs
{
	public class MessageHub : Hub
	{
		public async Task SendMessage(string Message)
		{
			await Clients.All.SendAsync("ResiveMessage", Message);
		}
	}
}
