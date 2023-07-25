using DiscordMusicBot.External_Classes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMusicBot.Commands
{
    public class GameCommands : BaseCommandModule
    {

        [Command("blackjack")]
        public async Task CardGame(CommandContext ctx)
        {
            var Random = new Random();
            var userCard = new CardBuilder(Random);
            var botCard = new CardBuilder(Random);

            var UserMessage = new DiscordEmbedBuilder()
            {
                Title = "Kullanıcı Kartı",
                Color = DiscordColor.Yellow,
                Description = "Kullanıcının kartı : " + userCard.SelectedCard,
            };
            await ctx.Channel.SendMessageAsync(UserMessage);


            var BotMessage = new DiscordEmbedBuilder()
            {
                Title = "Bot Kartı",
                Color = DiscordColor.Purple,
                Description = "Botun kartı : " + botCard.SelectedCard,
            };
            await ctx.Channel.SendMessageAsync(BotMessage);

            if (userCard.SelectedNumber > botCard.SelectedNumber)
            {
                // Kullanıcı Kazandı 

                var WinningMessage = new DiscordEmbedBuilder()
                {
                    Title = "** Kullanıcı Kazandı **",
                    Color = DiscordColor.Green,
                };

                await ctx.Channel.SendMessageAsync(WinningMessage);
                return;
            }


            else if (botCard.SelectedNumber > userCard.SelectedNumber)
            {
                // Bot Kazandı 

                var LosingMessage = new DiscordEmbedBuilder()
                {
                    Title = "** Bot Kazandı **",
                    Color = DiscordColor.Red,
                };
                await ctx.Channel.SendMessageAsync(LosingMessage);
                return;
            }

            else 
            {
                // Eşit

                var EqualMessage = new DiscordEmbedBuilder()
                {
                    Title = "** Kartlar Aynı **",
                    Color = DiscordColor.White,
                };
                await ctx.Channel.SendMessageAsync(EqualMessage);
                return;
            }
        }
    }
}
