using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DiscordBot.Testing;

// "/test1 status"
// "description 'Gets the current server status'"
[SlashCommandGroup("test1", "test description")]
public class CoryTextCommands : ApplicationCommandModule
{
	private ILogger<CoryTextCommands> _logger;
	private DiscordClient _discordClient;

	public CoryTextCommands(ILogger<CoryTextCommands> logger, DiscordClient discordClient)
	{
		_logger = logger;
		_discordClient = discordClient;
	}

	[SlashCommand("greet", "simple test greeting command")]
	public async Task GreetCommand2(InteractionContext ctx)
	{
		await ctx.CreateResponseAsync("Greetings! Thank you for executing me!");
	}

	[SlashCommand("Hello", "A friendly greeting")]
	public async Task Hello(InteractionContext context)
	{
		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
		{
			Content = "Howdy Doody 👋"
		});

		// edit the response
		await context.EditResponseAsync(new DiscordWebhookBuilder()
			.WithContent($"Howdy Doody 👋 \nHow are you? {DiscordEmoji.FromName(_discordClient, ":thinking:")}"));
	}	

	[SlashCommand("roll", "Roll a Number")]
	public async Task Roll6(InteractionContext context,
		[Option("max", "Maximum number you could roll, 2 would be like a 2 sided die or 50-50.")][Maximum(1000)][Minimum(2)] long max = 2)
	{
		int diceRoll = new Random().Next(1, (int)max);

		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new()
		{
			Content = $"🎲 Rolled a {diceRoll}"
		});
	}

	[SlashCommand("help", "Get all commands? maybe more info on a command?")]
	public async Task Help(InteractionContext context)
	{
		await context.CreateResponseAsync("no");
	}

	[SlashCommand("gif", "send gif test")]
	public async Task Gif(InteractionContext context)
	{
		await context.CreateResponseAsync(embed: new DiscordEmbedBuilder { ImageUrl = "https://i.giphy.com/media/irBHYSZxbUifTxTgBL/200w.webp" }.Build(), false);
	}	
}