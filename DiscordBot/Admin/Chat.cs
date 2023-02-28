using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace DiscordBot.Admin;

[SlashCommandGroup("Chat", "Commands for clearing chat and other admin needs.")]
[SlashCommandPermissions(Permissions.ManageMessages)]
[SlashRequirePermissions(Permissions.ManageMessages)]
internal class Chat : ApplicationCommandModule
{
    [SlashCommand("remove", "Clears chat with up to the 'limit' and allows 'skipping' x number of messages.")]
    public async Task RemoveAsync(InteractionContext context,
    [Option("limit", "Total amount of messages to remove")][Minimum(1)][Maximum(100)] long limit = 50,
    [Option("skip", "Amount of newer messages to skip")][Minimum(0)][Maximum(99)] long skip = 0)
    {
        IEnumerable<DiscordMessage> messages = (await context.Channel.GetMessagesAsync((int)limit)).Skip((int)skip);

        await PurgeAsync(context, messages);
    }

    /// <summary>
    /// Removes any chat content passed in.
    /// </summary>
    /// <param name="context">Context of Discord</param>
    /// <param name="messages">any messages that are to be deleted</param>
    /// <returns>success message to chat</returns>
	private async Task PurgeAsync(InteractionContext context, IEnumerable<DiscordMessage> messages)
    {
        var deleteable = messages;
        deleteable = deleteable.Where(x => DateTimeOffset.Now.Subtract(x.CreationTimestamp).TotalDays < 14).ToList();

        if (!deleteable.Any())
        {
            await context.CreateResponseAsync("⚠️ No messages were deleted. Take note that messages older than 14 days can not be deleted with purge.", true);
            return;
        }

        await context.Channel.DeleteMessagesAsync(deleteable, "RoboCory Purge");
        await context.CreateResponseAsync($"✅ Deleted {deleteable.Count()} messages.", true);
    }
}
