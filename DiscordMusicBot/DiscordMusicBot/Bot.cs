using DiscordMusicBot.Commands;
using DiscordMusicBot.Config;
using DiscordMusicBot.External_Classes;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMusicBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }

        public CommandsNextExtension Commands { get; private set; }


        public async Task RunAsync()
        {
            var configJsonFile = new ConfigJSONReader();

            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJsonFile.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJsonFile.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<GameCommands>();

            Commands.CommandErrored += OnCommandError; 

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private async Task OnCommandError(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if (e.Exception is ChecksFailedException)
            {
                var castedException = (ChecksFailedException)e.Exception;
                string coolDownTimer = string.Empty;

                foreach (var check in castedException.FailedChecks)
                {
                    var coolDown = (CooldownAttribute)check;
                    TimeSpan timeLeft = coolDown.GetRemainingCooldown(e.Context);
                   coolDownTimer = timeLeft.ToString(@"hh\:mm\:ss");
                }

                var coolDownMessage = new DiscordEmbedBuilder()
                {
                    Title = "Bekleme süresinin bitmesini bekleyin.",
                    Description = "Bekleme süresi :" + coolDownTimer,
                    Color = DiscordColor.Red
                };

                await e.Context.Channel.SendMessageAsync(coolDownMessage);
            }
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

    }
}