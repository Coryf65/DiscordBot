using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace DiscordBot;

[SlashCommandGroup("test1", "test description")]
public class CoryTextCommands : ApplicationCommandModule
{
	private ILogger<CoryTextCommands> _logger;
	private DiscordClient _discordClient;
	// private readonly MinecarftServer minecraftServer;

	// inject minecraft server here
	public CoryTextCommands(ILogger<CoryTextCommands> logger, DiscordClient discordClient)
	{
		_logger = logger;
		_discordClient = discordClient;
	}

	public async Task Hello(InteractionContext context)
	{
		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
		{
			Content = "Howdy Doody 👋"
		});

		// do something
		// await minecraftServer.StartAsync();

		// edit the response
		await context.EditResponseAsync(new DSharpPlus.Entities.DiscordWebhookBuilder()
			.WithContent("Howdy Doody 👋 \nHow are you?"));
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

		// test this
		//await context.CreateResponseAsync($"Status: {status}");
	}

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
}