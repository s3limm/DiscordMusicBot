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
    public class FunCommands : BaseCommandModule
    {
        [Command("test")]
        public async Task CommandTest(CommandContext ctx)
        {
            Console.WriteLine("Working...");
            await ctx.Channel.SendMessageAsync("Selam Paşşaaaaam");
        }

        [Command("selam")]
        public async Task CommandGreeting(CommandContext ctx)
        {
            Console.WriteLine("Working...");
            await ctx.Channel.SendMessageAsync("Naptın Ahmet");
        }

        [Command("küfür")]
        public async Task CommandRude(CommandContext ctx)
        {
            Console.WriteLine("Working...");
            await ctx.Channel.SendMessageAsync("Ya siktir git dedi ammmına goyarım ha");
        }


        [Command("topla")]
        public async Task Addition(CommandContext ctx, int number1, int number2)
        {
            int result = number1 + number2;
            await ctx.Channel.SendMessageAsync(result.ToString());
        }

        [Command("çıkart")]
        public async Task Subtract(CommandContext ctx, int number1, int number2)
        {
            int result = number1 - number2;
            await ctx.Channel.SendMessageAsync(result.ToString());
        }

        [Command("çarp")]
        public async Task Multipy(CommandContext ctx, int number1, int number2)
        {
            int result = number1 * number2;
            await ctx.Channel.SendMessageAsync(result.ToString());
        }

        [Command("böl")]
        public async Task Divide(CommandContext ctx, int number1, int number2)
        {
            int result = number1 / number2;
            await ctx.Channel.SendMessageAsync(result.ToString());
        }

        [Command("embedmessage")]
        public async Task EmbedMessage(CommandContext ctx)
        {
            var embedmessage = new DiscordEmbedBuilder()
            {
                Title = "Bu başlıktır.",
                Description = "Bu açıklamadır",
                Color = DiscordColor.Azure,
            };

            await ctx.Channel.SendMessageAsync(embedmessage);
        }
    }
}