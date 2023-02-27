using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Diagnostics;

namespace DiscordBot.BotInfo;

[SlashCommandGroup("bot", "Commands for info about RoboCory, data, info, version")]
internal class Info : ApplicationCommandModule
{
	[SlashCommand("Info","detailed info about the bot (RoboCory)")]
	public async Task BotInfo(InteractionContext context)
	{
		Process[] Processes = Process.GetProcesses();
		long tickCountMs = Environment.TickCount64;
		var uptime = TimeSpan.FromMilliseconds(tickCountMs);

		DiscordEmbedBuilder embed = new()
		{
			Color = DiscordColor.CornflowerBlue,
			Description =
			$"> Online for: `{uptime}`\n" +
			$"> Developed by {Formatter.MaskedUrl("Cory", new Uri("https://coryf.dev"), "Cory's Dev Website")}\n\n"
		};

		embed.AddField("**Discord**",
			$"> Running for `{context.Client.Guilds.Count}` server.\n");
			
		embed.AddField("**System**",
			$"> .NET: `{Environment.Version}`\n" +
			$"> OS:   `{Environment.OSVersion}`\n" +

			$"> Disk Space: `{DriveInfo.GetDrives().Sum(p => p.AvailableFreeSpace / 1024 / 1024 / 1024)}gb`\n" +
			$"> Total Disk Space: `{DriveInfo.GetDrives().Sum(p => p.TotalSize / 1024 / 1024 / 1024)}gb`\n" +
			$"> Ram: `{Processes.Where(p => p.ProcessName.Contains("discordbot")).Sum(p => p.WorkingSet64)}mb`\n", true);
		
		embed.AddField("**ping**",
			$"> Ping API: `{context.Client.Ping}ms`\n");

		embed.WithAuthor("Cory", "https://coryf.dev", context.Client.CurrentUser.AvatarUrl);

		await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
			   new DiscordInteractionResponseBuilder().AddEmbed(embed));

		return;
	}
}