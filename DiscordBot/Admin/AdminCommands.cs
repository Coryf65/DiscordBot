using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace DiscordBot.Admin;

[SlashCommandGroup("admin", "Commands for clearing chat and other admin needs.")]
[SlashCommandPermissions(Permissions.ManageMessages)]
[SlashRequirePermissions(Permissions.ManageMessages)]
internal class AdminCommands : ApplicationCommandModule
{
    [SlashCommand("regular", "Clears chat without any special parameters.")]
    public async Task RegularPurgeAsync(InteractionContext ctx,
    [Option("limit", "Maximum amount of messages to fetch in this Purge")][Maximum(100)][Minimum(1)] long limit = 50,
    [Option("skip", "Amount of newer messages to skip when purging")][Minimum(0)][Maximum(99)] long skip = 0)
    {
        IEnumerable<DiscordMessage> messages = (await ctx.Channel.GetMessagesAsync((int)limit)).Skip((int)skip);

        await deleteAsync(ctx, messages);
    }

    private async Task deleteAsync(InteractionContext ctx, IEnumerable<DiscordMessage> messages)
    {
        var deleteable = messages;
        deleteable = deleteable.Where(x => DateTimeOffset.Now.Subtract(x.CreationTimestamp).TotalDays < 14).ToList();

        if (!deleteable.Any())
        {
            await ctx.CreateResponseAsync("⚠️ No messages were deleted. Take note that messages older than 14 days can not be deleted with purge.", true);
            return;
        }

        await ctx.Channel.DeleteMessagesAsync(deleteable, "ModCore Purge");
        await ctx.CreateResponseAsync($"✅ Deleted {deleteable.Count()} messages.", true);
    }
}
