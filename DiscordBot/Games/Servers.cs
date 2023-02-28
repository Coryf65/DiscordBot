using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace DiscordBot.Games;

[SlashCommandGroup("servers", "Commands for info about the current Game Servers running or how to connect")]
internal class Servers
{
	[SlashCommand("info", "Get IP and Modpack for server")]
	public async Task GetServerInformation(InteractionContext context)
	{
		var ip = "127.0.0.1";
		// var (ip, domain) = await minecraftServer.GetConnectionInformation();

		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
		{
			Content = $"Server ip: {ip}"
		});
	}

	[SlashCommand("status", "Get the current server status")]
	public async Task Status(InteractionContext context)
	{
		//var status = await minecraftServer.GetStatusAsync();

		var status = "online!";

		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
		{
			Content = $"Server status is {status}"
		});

		//await context.CreateResponseAsync($"Status: {status}");
	}
}