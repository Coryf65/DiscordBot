using DiscordBot;
using DiscordBot.Admin;
using DiscordBot.BotInfo;
using DiscordBot.Testing;
using DSharpPlus;
using DSharpPlus.Exceptions;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Serilog;
using Serilog.Formatting.Json;

const ulong GUILD_ID = 253627388754264065;

// creating the Serilog ILogger
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File(path: "logs/logs.txt",
				restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
				rollingInterval: RollingInterval.Day) // setting file log levels to the file
	.WriteTo.File(new JsonFormatter(), path: "logs/jsonlogs.json",
				restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
				rollingInterval: RollingInterval.Day) // JSON formatter
	.WriteTo.File(path: "logs/errors.txt",
				restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning) // will only write Errors to this file
	.CreateLogger();

try
{
	Log.Information("Setting up Discord Bot");

	IHost host = Host.CreateDefaultBuilder(args)
	.UseSerilog()
	.ConfigureServices((builder, services) =>
	{
		// Facotry method, initializing client
		services.AddSingleton<DiscordClient>((serviceProvider) =>
		{
			// getting config files
			var configuration = builder.Configuration;

			var discordClient = new DiscordClient(new DiscordConfiguration
			{
				Token = configuration["DiscordBot:Token"],
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.AllUnprivileged
			});

			var slash = discordClient.UseSlashCommands(new SlashCommandsConfiguration
			{
				Services = serviceProvider
			});

			slash.RegisterCommands<CoryTextCommands>(guildId: GUILD_ID);
			slash.RegisterCommands<Info>(guildId: GUILD_ID);
			slash.RegisterCommands<Chat>(guildId: GUILD_ID);
			slash.SlashCommandErrored += Slash_SlashCommandErrored;

			return discordClient;
		});
		services.AddHostedService<Worker>();
	})
	.Build();

	await host.RunAsync();

}
catch (Exception error)
{
	// log any errors while setting up the host
	Log.Fatal(error, "Exception while setting up...");
}
finally
{
	// cleanup
	Log.Information("Shutting down DiscordBot...");
	Log.CloseAndFlush();
}


Task Slash_SlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
{
	Log.Error("Exception occured:\n" + e.Exception);
	switch (e.Exception)
	{
		case BadRequestException ex:
			Log.Error("Client BadRequestException:\n JSON Message: " + ex.JsonMessage);
			break;
		default:
			Log.Error("Error: {error}", e.Exception);
			break;
	}
	return Task.CompletedTask;
}