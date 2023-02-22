using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBot;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly DiscordClient _discordClient;

	public Worker(ILogger<Worker> logger, DiscordClient discordClient)
	{
		_logger = logger;
		_discordClient = discordClient;
	}

	/// <summary>
	/// Start the discord client
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public override async Task StartAsync(CancellationToken cancellationToken)
	{
		// listen to the events os messages being created
		_discordClient.MessageCreated += OnMessageCreated;

		await _discordClient.ConnectAsync();
	}

	public async Task OnMessageCreated(DiscordClient discordClient, MessageCreateEventArgs e)
	{
		// check message contents for a command we can react to

		// hello world!, old way to do this need to enable Message Content Intent on discord dev portal
		if (e.Message.Content == "Ping")
		{
			await e.Message.RespondAsync("Pong!");
		}
	}

	/// <summary>
	/// Stop the discord client
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public override async Task StopAsync(CancellationToken cancellationToken)
	{
		_discordClient.MessageCreated -= OnMessageCreated;
		await _discordClient.DisconnectAsync();
		_discordClient.Dispose();
	}

	/// <summary>
	/// Won't implement this we will be doing this in a different way
	/// </summary>
	/// <param name="stoppingToken"></param>
	/// <returns></returns>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await Task.CompletedTask;
	}
}