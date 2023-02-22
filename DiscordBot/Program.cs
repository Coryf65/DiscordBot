using DiscordBot;
using DSharpPlus;

IHost host = Host.CreateDefaultBuilder(args)
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
			return discordClient;
		});
		services.AddHostedService<Worker>();
	})
	.Build();

await host.RunAsync();
