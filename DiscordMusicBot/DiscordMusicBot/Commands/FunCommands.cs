using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMusicBot.Commands
{
    [Cooldown(5, 10, CooldownBucketType.User)]
    public class FunCommands : BaseCommandModule
    {
        [Command("test")]
       
        public async Task CommandTest(CommandContext ctx)
        {
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

            await ctx.RespondAsync(embedmessage);
        }



        [Command("anket")]
        public async Task Poll(CommandContext ctx, int TimeLimit, string Option1, string Option2, string Option3, string Option4, string Question)
        {
            var interactvity = ctx.Client.GetInteractivity(); // Kullanıcıdan değer alındığı kısım.
            TimeSpan timer = TimeSpan.FromSeconds(TimeLimit); // Sayacın oluşturulduğu kısım.

            DiscordEmoji[] optionEmojis ={ DiscordEmoji.FromName(ctx.Client, ":one:", false),
                                           DiscordEmoji.FromName(ctx.Client, ":two:", false),
                                           DiscordEmoji.FromName(ctx.Client, ":three:", false),
                                           DiscordEmoji.FromName(ctx.Client, ":four:", false)};


            string optionStrings = optionEmojis[0] + " | " + Option1 + "\n" +
                                   optionEmojis[1] + " | " + Option2 + "\n" +
                                   optionEmojis[2] + " | " + Option3 + "\n" +
                                   optionEmojis[3] + " | " + Option4 ;

            var pollMessage = new DiscordEmbedBuilder()
            {
                Title = " " + Question,
                Description = optionStrings,
                Color = DiscordColor.Azure
            };

            var putReactOn = await ctx.Channel.SendMessageAsync(pollMessage);

            foreach (var emoji in optionEmojis)
            {
                await putReactOn.CreateReactionAsync(emoji);
            }

            var result = await interactvity.CollectReactionsAsync(putReactOn, timer);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach (var emoji in result)
            {
                if(emoji.Emoji == optionEmojis[0])
                {
                    count1++;
                }

                if (emoji.Emoji == optionEmojis[1])
                {
                    count2++;
                }
                
                if (emoji.Emoji == optionEmojis[2])
                {
                    count3++;
                }

                if (emoji.Emoji == optionEmojis[3])
                {
                    count4++;
                }
            }

            int totalVotes = count1 + count2 + count3 + count4;


            string resultString = optionEmojis[0] + ":  " +  count1 + " Oy \n" +
                optionEmojis[1] + ":  " + count2 + " Oy \n" +
                optionEmojis[2] + ":  " + count3 + " Oy \n" +
                optionEmojis[3] + ":  " + count4 + " Oy \n\n" + "Toplam oy sayısı : " + totalVotes;

            var resultMessage = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Green,
                Title = "Oylamanın sonucu",
                Description = resultString
                
            };

            await ctx.Channel.SendMessageAsync(resultMessage);

        }
    }
}
