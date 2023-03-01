# DiscordBot

A Discord Bot that will run for my Discord Server and friends!

Will contain some useful info for us like Rolls, Server info, Games, and other things we may need!

## Tools

Runs in a Linux Docker Container, you will need to add api token and start setup in Discord [here](https://discord.com/developers/docs/getting-started)

- [Discord API](https://discord.com/developers/docs/intro) allows cool bots and other things
- [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus) used to interact with Discord API with C#
- [DSharp Slash Commands](https://dsharpplus.github.io/DSharpPlus/articles/slash_commands.html) The newer commands way to interact with /command
- [Serilog](https://dsharpplus.github.io/DSharpPlus/articles/slash_commands.html) used for Logging

## Commands

Some example commands [here](https://dsharpplus.github.io/DSharpPlus/articles/commands/intro.html)
Command Custom Formatting [here](https://dsharpplus.github.io/DSharpPlus/articles/commands/help_formatter.html)

- all current commands and planned ones...

> TODO...

### Notes

Allowed parameters to the slash function commands are...

- string
- long
- bool
- double
- TimeSpan?
- DiscordChannel
- DiscordUser
- DiscordRole
- DiscordEmoji
- DiscordAttachment
- SnowflakeObject
- Enum