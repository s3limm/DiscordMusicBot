using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
            await ctx.Channel.SendMessageAsync("Hello");
        }


        [Command("add")]
        public async Task Addition(CommandContext ctx , int number1 , int number2)
        {
            int result = number1 + number2;
            await ctx.Channel.SendMessageAsync(result.ToString());
        }

        [Command("subtract")]
        public async Task Subtract(CommandContext ctx, int number1, int number2)
        {
            int result = number1 - number2;
            await ctx.Channel.SendMessageAsync(result.ToString());
        }
    }
}


